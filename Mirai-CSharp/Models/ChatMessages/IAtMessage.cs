namespace Mirai.CSharp.Models.ChatMessages
{
    /// <summary>
    /// 表示 @特定对象 消息的基接口
    /// </summary>
    public interface IAtMessage : IChatMessage
    {
        /// <summary>
        /// 被@的群员QQ号
        /// </summary>
        long Target { get; }
        /// <summary>
        /// At时显示的文字, 发送消息时无效, 自动使用群名片
        /// </summary>
        string? Display { get; }
    }
}
