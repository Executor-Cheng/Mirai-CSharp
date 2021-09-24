using System.Threading.Tasks;
using Mirai.CSharp.HttpApi.Session;
using Mirai.CSharp.Models;
using Mirai.CSharp.Models.ChatMessages;

#pragma warning disable IDE0051 // Remove unused private members
#pragma warning disable CA1822 // Mark members as static
namespace Mirai.CSharp.Example
{
    public partial class ExamplePlugin
    {
        private async Task SendPictureAsync(IMiraiHttpSession session, string path) // 发图
        {
            // 你也可以使用另一个重载 UploadPictureAsync(PictureTarget, Stream)
            // 当 mirai-api-http 在v1.7.0以下时将使用本地的HttpListener做图片中转
            // UploadTarget.Friend 对应 IMiraiSession.SendFriendMessageAsync
            IImageMessage msg = await session.UploadPictureAsync(UploadTarget.Group, path);
            IChatMessage[] chain = new IChatMessage[] { msg }; // 数组里边可以加上更多的 IMessageBase, 以此达到例如图文并发的情况
            await session.SendGroupMessageAsync(0, chain); // 自己填群号, 一般由 IGroupMessageEventArgs 提供
        }

        private async Task SendPictureAsync2(IMiraiHttpSession session, string path) // 发图, 不同的是使用 IMessageChainBuilder 构造消息链
        {
            IImageMessage msg = await session.UploadPictureAsync(UploadTarget.Group, path);
            Builders.IMessageChainBuilder builder = session.GetMessageChainBuilder();
            builder.Add(msg);
            await session.SendGroupMessageAsync(0, builder); // 自己填群号, 一般由 IGroupMessageEventArgs 提供
        }
    }
}
