using System.Threading;
using System.Threading.Tasks;
using Mirai_CSharp.Models;

namespace Mirai_CSharp.Session
{
    public abstract partial class MiraiSession
    {
        public abstract Task RevokeMessageAsync(int messageId, CancellationToken token = default);

        public abstract Task<int> SendFriendMessageAsync(long qqNumber, IMessageBase[] chain, CancellationToken token = default);

        public abstract Task<int> SendFriendMessageAsync(long qqNumber, IMessageBuilder builder, CancellationToken token = default);

        public abstract Task<int> SendFriendMessageAsync(long qqNumber, IMessageBase[] chain, int? quoteMsgId, CancellationToken token = default);

        public abstract Task<int> SendFriendMessageAsync(long qqNumber, IMessageBuilder builder, int? quoteMsgId, CancellationToken token = default);

        public abstract Task<int> SendGroupMessageAsync(long groupNumber, IMessageBase[] chain, CancellationToken token = default);

        public abstract Task<int> SendGroupMessageAsync(long groupNumber, IMessageBuilder builder, CancellationToken token = default);

        public abstract Task<int> SendGroupMessageAsync(long groupNumber, IMessageBase[] chain, int? quoteMsgId, CancellationToken token = default);

        public abstract Task<int> SendGroupMessageAsync(long groupNumber, IMessageBuilder builder, int? quoteMsgId, CancellationToken token = default);

        public abstract Task<int> SendTempMessageAsync(long qqNumber, long groupNumber, IMessageBase[] chain, CancellationToken token = default);

        public abstract Task<int> SendTempMessageAsync(long qqNumber, long groupNumber, IMessageBuilder builder, CancellationToken token = default);

        public abstract Task<int> SendTempMessageAsync(long qqNumber, long groupNumber, IMessageBase[] chain, int? quoteMsgId, CancellationToken token = default);

        public abstract Task<int> SendTempMessageAsync(long qqNumber, long groupNumber, IMessageBuilder builder, int? quoteMsgId, CancellationToken token = default);
    }
}
