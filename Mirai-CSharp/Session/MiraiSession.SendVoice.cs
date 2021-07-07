using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Mirai_CSharp.Models;

namespace Mirai_CSharp.Session
{
    public abstract partial class MiraiSession
    {
        public abstract Task<VoiceMessage> UploadVoiceAsync(UploadTarget type, Stream voice, CancellationToken token = default);

        public abstract Task<VoiceMessage> UploadVoiceAsync(UploadTarget type, string voicePath, CancellationToken token = default);
    }
}
