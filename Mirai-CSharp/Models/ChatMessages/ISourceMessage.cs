using System;

namespace Mirai.CSharp.Models.ChatMessages
{
    /// <summary>
    /// 表示消息基本信息的基接口
    /// </summary>
    public interface ISourceMessage : IChatMessage
    {
        /// <summary>
        /// 消息的识别号, 用于引用回复或撤回
        /// </summary>
        int Id { get; }
        /// <summary>
        /// 消息时间
        /// </summary>
        DateTime Time { get; }
    }
}
