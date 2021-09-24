namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供用于处理相关申请所需信息的接口
    /// </summary>
    public interface IApplyResponseArgs : IMiraiMessage
    {
        /// <summary>
        /// 事件Id, 供 mirai-api-http 使用
        /// </summary>
        long EventId { get; }

        /// <summary>
        /// 申请人QQ号
        /// </summary>
        long FromQQ { get; }

        /// <summary>
        /// 申请来源群号
        /// </summary>
        /// <remarks>
        /// 在好友添加事件中, 如果申请人通过某个群添加好友, 该项为该群群号, 否则为0
        /// </remarks>
        long FromGroup { get; }
    }

    /// <summary>
    /// 提供用于处理相关申请所需信息的接口。继承自 <see cref="IApplyResponseArgs"/> 和 <see cref="IMiraiMessage{TRawdata}"/>
    /// </summary>
    public interface IApplyResponseArgs<TRawdata> : IApplyResponseArgs, IMiraiMessage<TRawdata>
    {
        
    }
}
