using Mirai_CSharp.Models;
using System.Threading.Tasks;

#pragma warning disable IDE0051 // 删除未使用的私有成员
namespace Mirai_CSharp.Example
{
    public partial class ExamplePlugin
    {
        private async Task SendPictureAsync(MiraiHttpSession session, string path) // 发图
        {
            // 你也可以使用另一个重载 UploadPictureAsync(PictureTarget, Stream)
            // mirai-api-http 在v1.7.0以下时将使用本地的HttpListener做图片中转
            ImageMessage msg = await session.UploadPictureAsync(PictureTarget.Group, path);
            IMessageBase[] chain = new IMessageBase[] { msg }; // 数组里边可以加上更多的 IMessageBase, 以此达到例如图文并发的情况
            await session.SendGroupMessageAsync(0, chain); // 自己填群号, 一般由 IGroupMessageEventArgs 提供
        }
    }
}
