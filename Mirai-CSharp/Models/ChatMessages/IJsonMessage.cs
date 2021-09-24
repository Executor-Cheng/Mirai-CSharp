namespace Mirai.CSharp.Models.ChatMessages
{
    /// <summary>
    /// 表示Json消息的基接口
    /// </summary>
    public interface IJsonMessage : IChatMessage
    {
        /// <summary>
        /// Json原始字符串
        /// </summary>
        string Json { get; }
    }
}
