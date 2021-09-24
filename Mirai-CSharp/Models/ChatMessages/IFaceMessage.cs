namespace Mirai.CSharp.Models.ChatMessages
{
    /// <summary>
    /// 表示一个QQ表情的基接口
    /// </summary>
    public interface IFaceMessage : IChatMessage
    {
        /// <summary>
        /// QQ表情编号, 可选, 优先高于 <see cref="Name"/>
        /// </summary>
        /// <remarks>
        /// 编号详见 <a href="https://github.com/mamoe/mirai/blob/master/mirai-core/src/commonMain/kotlin/net.mamoe.mirai/message/data/Face.kt#L41"/>
        /// </remarks>
        int Id { get; }
        /// <summary>
        /// QQ表情拼音, 可选
        /// </summary>
        string? Name { get; }
    }
}
