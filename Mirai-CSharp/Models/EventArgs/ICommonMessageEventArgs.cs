using Mirai.CSharp.Models.ChatMessages;

namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供通用消息的相关信息接口
    /// </summary>
    public interface ICommonMessageEventArgs : IMiraiMessage
    {
        /// <summary>
        /// 消息链数组
        /// </summary>
        IChatMessage[] Chain { get; }
    }

    /// <summary>
    /// 提供通用消息的相关信息接口。继承自 <see cref="ICommonMessageEventArgs"/> 和 <see cref="IMiraiMessage{TRawdata}"/>
    /// </summary>
    public interface ICommonMessageEventArgs<TRawdata> : ICommonMessageEventArgs, IMiraiMessage<TRawdata>
    {
        
    }
}
