namespace Mirai.CSharp.Models.ChatMessages
{
    /// <summary>
    /// 表示商城表情消息的基接口
    /// </summary>
    public interface IMarketFaceMessage : IChatMessage
    {
        /// <summary>
        /// 商城表情唯一标识
        /// </summary>
        int Id { get; }
        /// <summary>
        /// 表情显示名称
        /// </summary>
        string Name { get; }
    }
}
