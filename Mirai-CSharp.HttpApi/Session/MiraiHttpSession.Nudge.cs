using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Extensions;
using Mirai.CSharp.HttpApi.Extensions;
using Mirai.CSharp.Models;
#if NET5_0_OR_GREATER
using System.Net.Http.Json;
#endif

namespace Mirai.CSharp.HttpApi.Session
{
    public partial class MiraiHttpSession
    {
        /// <inheritdoc/>
        public override Task NudgeAsync(NudgeTarget target, long qqNumber, long? groupNumber = null, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            var payload = new
            {
                sessionKey = session.SessionKey,
                target = qqNumber,
                subject = groupNumber ?? qqNumber,
                kind = target.ToString()
            };
            return _client.PostAsJsonAsync($"{_options.BaseUrl}/sendNudge", payload, token)
                .AsApiRespAsync(token)
                .DisposeWhenCompleted(cts);
        }
    }
}
