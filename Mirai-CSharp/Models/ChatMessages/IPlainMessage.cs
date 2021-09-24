namespace Mirai.CSharp.Models.ChatMessages
{
    /// <summary>
    /// 表示文字消息的基接口
    /// </summary>
    public interface IPlainMessage : IChatMessage
    {
        /// <summary>
        /// 文字消息
        /// </summary>
        string Message { get; }
    }
}
