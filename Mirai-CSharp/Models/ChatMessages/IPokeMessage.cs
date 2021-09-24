namespace Mirai.CSharp.Models.ChatMessages
{
    /// <summary>
    /// 表示戳一戳消息的基接口
    /// </summary>
    public interface IPokeMessage : IChatMessage
    {
        /// <summary>
        /// 戳一戳类型
        /// </summary>
        PokeType Name { get; }
    }
}
