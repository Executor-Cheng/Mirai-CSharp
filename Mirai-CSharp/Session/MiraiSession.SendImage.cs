using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Models;
using Mirai.CSharp.Models.ChatMessages;

namespace Mirai.CSharp.Session
{
    public abstract partial class MiraiSession
    {
        public abstract Task<string[]> SendImageToFriendAsync(long qqNumber, string[] urls, CancellationToken token = default);

        public abstract Task<string[]> SendImageToGroupAsync(long groupNumber, string[] urls, CancellationToken token = default);

        public abstract Task<string[]> SendImageToTempAsync(long qqNumber, long groupNumber, string[] urls, CancellationToken token = default);

        public abstract Task<IImageMessage> UploadPictureAsync(UploadTarget type, Stream image, CancellationToken token = default);

        public abstract Task<IImageMessage> UploadPictureAsync(UploadTarget type, string imagePath, CancellationToken token = default);
    }
}
