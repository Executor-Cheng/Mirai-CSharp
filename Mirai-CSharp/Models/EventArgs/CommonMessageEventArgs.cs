namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供通用消息的相关信息接口
    /// </summary>
    public interface ICommonMessageEventArgs : IEventArgsBase
    {
        /// <summary>
        /// 消息链数组
        /// </summary>
        IMessageBase[] Chain { get; }
    }

    /// <summary>
    /// 通用消息的相关信息基类
    /// </summary>
    public abstract class CommonMessageEventArgs : EventArgsBase, ICommonMessageEventArgs
    {
        /// <summary>
        /// 消息链数组
        /// </summary>
        public IMessageBase[] Chain { get; set; } = null!;

        protected CommonMessageEventArgs() { }

        protected CommonMessageEventArgs(IMessageBase[] chain)
        {
            Chain = chain;
        }
    }
}
