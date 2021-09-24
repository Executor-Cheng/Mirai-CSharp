namespace Mirai.CSharp.Models.ChatMessages
{
    /// <summary>
    /// 表示摇骰子消息的基接口
    /// </summary>
    public interface IDiceMessage : IChatMessage
    {
        /// <summary>
        /// 点数
        /// </summary>
        int Point { get; }
    }
}
