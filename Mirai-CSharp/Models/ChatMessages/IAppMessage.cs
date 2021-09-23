namespace Mirai.CSharp.Models.ChatMessages
{
    /// <summary>
    /// 表示App消息的基接口
    /// </summary>
    public interface IAppMessage : IChatMessage
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        string Content { get; }
    }
}
