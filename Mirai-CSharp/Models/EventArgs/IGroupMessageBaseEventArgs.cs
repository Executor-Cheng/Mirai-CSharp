using System.Linq;

namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供群消息和临时消息的相关信息基接口。继承自 <see cref="ICommonMessageEventArgs"/>
    /// </summary>
    public interface IGroupMessageBaseEventArgs : ICommonMessageEventArgs
    {
        /// <summary>
        /// 消息发送者信息
        /// </summary>
        IGroupMemberInfo Sender { get; }
    }

    /// <summary>
    /// 提供群消息和临时消息的相关信息基接口。继承自 <see cref="IGroupMessageBaseEventArgs"/> 和 <see cref="ICommonMessageEventArgs{TRawdata}"/>
    /// </summary>
    public interface IGroupMessageBaseEventArgs<TRawdata> : IGroupMessageBaseEventArgs, ICommonMessageEventArgs<TRawdata>
    {
        
    }
}
