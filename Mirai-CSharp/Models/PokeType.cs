namespace Mirai.CSharp.Models
{
    /// <summary>
    /// 戳一戳的类型。
    /// </summary>
    /// <remarks>
    /// SVIP的Poke带Id, <see langword="enum"/> 无法表示两个值, 不写。
    /// 详见 <a href="https://github.com/mamoe/mirai/blob/8ca4357eb834f3c284deb68a6dd25d5c59957a82/mirai-core/src/commonMain/kotlin/net.mamoe.mirai/message/data/HummerMessage.kt#L56"/>
    /// </remarks>
    public enum PokeType
    {
        /// <summary>
        /// 戳一戳
        /// </summary>
        Poke = 1,
        /// <summary>
        /// 比心
        /// </summary>
        ShowLove,
        /// <summary>
        /// 点赞
        /// </summary>
        Like,
        /// <summary>
        /// 心碎
        /// </summary>
        Heartbroken,
        /// <summary>
        /// 666
        /// </summary>
        SixSixSix,
        /// <summary>
        /// 放大招
        /// </summary>
        FangDaZhao,
    }
}
