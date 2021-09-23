using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Models;
using Mirai.CSharp.Models.ChatMessages;

namespace Mirai.CSharp.Session
{
    public abstract partial class MiraiSession
    {
        public abstract Task<IVoiceMessage> UploadVoiceAsync(UploadTarget type, Stream voice, CancellationToken token = default);

        public abstract Task<IVoiceMessage> UploadVoiceAsync(UploadTarget type, string voicePath, CancellationToken token = default);
    }
}
