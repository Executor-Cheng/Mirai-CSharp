namespace Mirai.CSharp.Models.ChatMessages
{
    /// <summary>
    /// 表示xml消息的基接口
    /// </summary>
    public interface IXmlMessage : IChatMessage
    {
        /// <summary>
        /// Xml原始字符串
        /// </summary>
        string Xml { get; }
    }
}
