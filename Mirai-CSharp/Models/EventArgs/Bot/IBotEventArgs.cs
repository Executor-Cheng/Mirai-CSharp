namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 机器人本身事件接口。仅含QQ号
    /// </summary>
    public interface IBotEventArgs : IMiraiMessage
    {
        /// <summary>
        /// 机器人QQ号
        /// </summary>
        long QQNumber { get; }
    }

    /// <inheritdoc/>
    public interface IBotEventArgs<TRawdata> : IBotEventArgs, IMiraiMessage<TRawdata>
    {
        
    }
}
