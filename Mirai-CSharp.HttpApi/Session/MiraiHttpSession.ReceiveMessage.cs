using System;
using System.IO;
using System.Net.WebSockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.HttpApi.Options;
using Mirai.CSharp.HttpApi.Utility;

namespace Mirai.CSharp.HttpApi.Session
{
    public partial class MiraiHttpSession
    {
        private async Task StartReceiveMessageLoopAsync(MiraiHttpSessionOptions options, InternalSessionInfo session, CancellationToken connectToken, CancellationToken token)
        {
            ClientWebSocket ws = new ClientWebSocket();
            try
            {
                await ws.ConnectAsync(new Uri($"ws://{_options.Host}:{_options.Port}/all?verifyKey={options.AuthKey}&sessionKey={session.SessionKey}"), connectToken).ConfigureAwait(false);
                ReceiveMessageLoop(ws, session, token);
            }
            catch
            {
                ws.Dispose();
                throw;
            }
        }

        private async Task StartReceiveCommandLoopAsync(MiraiHttpSessionOptions options, InternalSessionInfo session, CancellationToken connectToken, CancellationToken token)
        {
            ClientWebSocket ws = new ClientWebSocket();
            try
            {
                await ws.ConnectAsync(new Uri($"ws://{_options.Host}:{_options.Port}/command?verifyKey={options.AuthKey}&sessionKey={session.SessionKey}"), connectToken).ConfigureAwait(false);
                ReceiveMessageLoop(ws, session, token);
            }
            catch
            {
                ws.Dispose();
                throw;
            }
        }

        private async void ReceiveMessageLoop(WebSocket ws, InternalSessionInfo session, CancellationToken token)
        {
            try
            {
                bool v2 = session.ApiVersion.Major >= 2;
                using MemoryStream stream = new MemoryStream(2048);
                while (true)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    await ws.ReceiveFullyAsync(stream, token).ConfigureAwait(false);
                    JsonElement root = JsonSerializer.Deserialize<JsonElement>(new ReadOnlySpan<byte>(stream.GetBuffer(), 0, (int)stream.Position));
                    if (v2)
                    {
                        root = root.GetProperty("data");
                    }
                    try
                    {
                        await _invoker.HandleRawdataAsync(this, root).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (OperationCanceledException)
            {

            }
            catch (Exception e)
            {
                if (session.Connected)
                {
                    _ = InternalReleaseAsync(session, e);
                }
            }
            finally
            {
                ws.Dispose();
            }
        }
    }
}
