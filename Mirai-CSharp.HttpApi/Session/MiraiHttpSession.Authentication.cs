using Mirai.CSharp.Extensions;
using Mirai.CSharp.HttpApi.Models.EventArgs;
using Mirai.CSharp.HttpApi.Extensions;
using Mirai.CSharp.HttpApi.Options;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Models.ChatMessages;
#if NET5_0_OR_GREATER
using System.Net.Http.Json;
#endif

#pragma warning disable CS0618 // Type or member is obsolete
#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
namespace Mirai.CSharp.HttpApi.Session
{
    public partial class MiraiHttpSession
    {
        /// <inheritdoc/>
        public Task ConnectAsync(long qqNumber, CancellationToken token = default)
        {
            return ConnectAsync(qqNumber, false, token);
        }

        /// <inheritdoc/>
        public async Task ConnectAsync(long qqNumber, bool listenCommand, CancellationToken token = default)
        {
            CancellationTokenSource? instanceCts = Volatile.Read(ref _instanceCts);
            CancellationToken instanceToken = instanceCts?.Token ?? throw new ObjectDisposedException(this.GetType().Name);
            CancellationToken createdToken;
            InternalSessionInfo? previousSession = Volatile.Read(ref _currentSession);
            InternalSessionInfo? createdSession = null;
            if (previousSession != null ||
                Interlocked.CompareExchange(ref _currentSession, createdSession = new InternalSessionInfo(instanceToken), null) != null)
            {
                createdSession?.Dispose();
                throw new InvalidOperationException();
            }
            createdToken = createdSession.Token;
            using CancellationTokenSource tempCts = CancellationTokenSource.CreateLinkedTokenSource(createdToken, token);
            CancellationToken tempToken = tempCts.Token;
            try
            {
                Version apiVersion = await GetVersionAsync(_client, _options, tempToken).ConfigureAwait(false);
                createdSession.ApiVersion = apiVersion;
                createdSession.QQNumber = qqNumber;
                createdSession.SessionKey = await AuthorizeAsync(_client, _options, apiVersion, tempToken).ConfigureAwait(false);
                await VerifyAsync(_client, _options, apiVersion, createdSession, tempToken).ConfigureAwait(false);
                bool v1 = apiVersion.Major < 2;
                if (v1)
                {
                    IMiraiSessionConfig config = await this.GetConfigAsync(createdSession, tempToken).ConfigureAwait(false);
                    if (!config.EnableWebSocket.GetValueOrDefault())
                    {
                        await this.SetConfigAsync(createdSession, new MiraiSessionConfig { CacheSize = config.CacheSize, EnableWebSocket = true }, tempToken).ConfigureAwait(false);
                    }
                }
                Task t1 = StartReceiveMessageLoopAsync(_options, createdSession, tempToken, createdToken);
                // mirai-api-http v2.0起, 指令ws合并进all, 所以只需连接上边这个就可以了
                Task t2 = v1 && listenCommand ? StartReceiveCommandLoopAsync(_options, createdSession, tempToken, createdToken) : Task.CompletedTask;
                await Task.WhenAll(t1, t2).ConfigureAwait(false);
                createdSession.Connected = true;
            }
            catch
            {
                if (Interlocked.CompareExchange(ref _currentSession, null!, createdSession) == createdSession) // 避免 StartReceiveMessageLoopAsync 和
                                                                                                               // StartReceiveCommandLoopAsync 中间出现连接断开
                                                                                                               // 导致释放两次 session
                {
                    tempCts.Cancel();
                    createdSession.Dispose();
                    if (createdSession.SessionKey != null)
                    {
                        _ = InternalReleaseAsync(createdSession.SessionKey, qqNumber, instanceToken);
                    }
                }
                throw;
            }
        }

        private abstract class AuthorizePayload
        {
            [JsonIgnore]
            public abstract string AuthKey { get; }

            protected AuthorizePayload()
            {
                
            }
        }

        private class AuthorizePayloadv1 : AuthorizePayload
        {
            [JsonPropertyName("authKey")]
            public override string AuthKey { get; }

            public AuthorizePayloadv1(string authKey)
            {
                AuthKey = authKey;
            }
        }

        private class AuthorizePayloadv2 : AuthorizePayload
        {
            [JsonPropertyName("verifyKey")]
            public override string AuthKey { get; }

            public AuthorizePayloadv2(string verifyKey) // 禁止重命名此参数
            {
                AuthKey = verifyKey;
            }
        }

        private static async Task<string> AuthorizeAsync(HttpClient client, MiraiHttpSessionOptions options, Version apiVersion, CancellationToken token = default)
        {
            //https://github.com/project-mirai/mirai-api-http/blob/master/docs/misc/Migration2.md#%E8%AE%A4%E8%AF%81%E6%B5%81%E7%A8%8B
            string url;
            AuthorizePayload payload;
            if (apiVersion.Major >= 2)
            {
                url = $"{options.BaseUrl}/verify";
                payload = new AuthorizePayloadv2(options.AuthKey);
            }
            else
            {
                url = $"{options.BaseUrl}/auth";
                payload = new AuthorizePayloadv1(options.AuthKey);
            };
            using JsonDocument j = await client.PostAsJsonAsync<object>(url, payload, token).GetJsonAsync(token).ConfigureAwait(false);
            JsonElement root = j.RootElement;
            root.EnsureApiRespCode();
            return root.GetProperty("session").GetString()!;
        }

        private static Task VerifyAsync(HttpClient client, MiraiHttpSessionOptions options, Version apiVersion, InternalSessionInfo session, CancellationToken token = default)
        {
            var payload = new
            {
                sessionKey = session.SessionKey,
                qq = session.QQNumber
            };
            string url = apiVersion.Major >= 2 ? $"{options.BaseUrl}/bind" : $"{options.BaseUrl}/verify";
            return client.PostAsJsonAsync(url, payload, token).AsApiRespAsync(token);
        }

        /// <param name="client">要进行请求的 <see cref="HttpClient"/></param>
        /// <param name="options">连接信息</param>
        /// <inheritdoc cref="GetVersionAsync(CancellationToken)"/>
        public static async Task<Version> GetVersionAsync(HttpClient client, MiraiHttpSessionOptions options, CancellationToken token = default)
        {
            using JsonDocument j = await client.GetAsync($"{options.BaseUrl}/about", token).GetJsonAsync(token).ConfigureAwait(false);
            JsonElement root = j.RootElement;
            root.EnsureApiRespCode();
            string version = root.GetProperty("data").GetProperty("version").GetString()!;
            int vIndex = version.IndexOf('v');
#if NETSTANDARD2_0
            return Version.Parse(vIndex != -1 ? version.Substring(vIndex) : version); // v1.0.0 ~ v1.7.4, skip 'v'
#else
            return Version.Parse(vIndex != -1 ? version.AsSpan()[(vIndex + 1)..] : version); // v1.0.0 ~ v1.7.4, skip 'v'
#endif
        }

        /// <inheritdoc cref="GetVersionAsync(CancellationToken)"/>
        public static Task<Version> GetVersionAsync(MiraiHttpSessionOptions options, CancellationToken token = default)
        {
            return GetVersionAsync(_globalClient, options, token);
        }

        /// <inheritdoc/>
        public Task<Version> GetVersionAsync(CancellationToken token)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            return GetVersionAsync(_client, _options, token).DisposeWhenCompleted(cts);
        }

        /// <summary>
        /// 异步释放Session
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task ReleaseAsync(CancellationToken token = default)
        {
            CheckDisposed();
            InternalSessionInfo? session = Interlocked.Exchange(ref _currentSession, null!);
            if (session == null)
            {
                throw new InvalidOperationException("请先连接到一个Session。");
            }
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            return InternalReleaseAsync(session.SessionKey, session.QQNumber, token).DisposeWhenCompleted(cts);
        }

        private Task InternalReleaseAsync(InternalSessionInfo session, Exception? e = null)
        {
            if (Interlocked.CompareExchange(ref _currentSession, null, session) == session)
            {
                if (e != null)
                {
                    _ = _invoker.HandleMessageAsync<IDisconnectedEventArgs>(this, new DisconnectedEventArgs(e, session.QQNumber));
                }
                return InternalReleaseAsync(session.SessionKey, session.QQNumber).DisposeWhenCompleted(session);
            }
            return Task.CompletedTask;
        }

        private Task InternalReleaseAsync(string sessionKey, long qqNumber, CancellationToken token = default)
        {
            var payload = new
            {
                sessionKey,
                qq = qqNumber
            };
            return _client.PostAsJsonAsync($"{_options.BaseUrl}/release", payload, token).AsApiRespAsync(token);
        }
    }
}
