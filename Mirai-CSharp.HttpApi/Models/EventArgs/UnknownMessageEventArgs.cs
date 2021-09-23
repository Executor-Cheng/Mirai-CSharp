using ISharedUnknownMessageEventArgs = Mirai.CSharp.Models.EventArgs.IUnknownMessageEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供未知消息的相关信息接口。继承自 <see cref="Mirai.CSharp.Models.EventArgs.IUnknownMessageEventArgs{TRawdata}"/> 和 <see cref="IMiraiHttpMessage"/>
    /// </summary>
    public interface IUnknownMessageEventArgs : ISharedUnknownMessageEventArgs, IMiraiHttpMessage
    {

    }

    public class UnknownMessageEventArgs : MiraiHttpMessage, IUnknownMessageEventArgs
    {
        
    }
}
