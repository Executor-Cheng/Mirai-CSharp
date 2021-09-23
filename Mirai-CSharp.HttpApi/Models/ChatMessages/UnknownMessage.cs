using System;
using System.Text.Json;

namespace Mirai.CSharp.HttpApi.Models.ChatMessages
{
    /// <summary>
    /// 表示未知消息的消息接口
    /// </summary>
    public interface IUnknownChatMessage : IChatMessage
    {

    }

    public class UnknownChatMessage
        : ChatMessage, IUnknownChatMessage
    {
        /// <summary>
        /// 本消息类不可序列化, 故不支持获取值
        /// </summary>
        public override string Type => throw new NotSupportedException();
        /// <summary>
        /// 初始化 <see cref="UnknownChatMessage"/> 类的新实例
        /// </summary>
        [Obsolete("本类不应由用户进行实例化。")]
        public UnknownChatMessage()
        {
            
        }
        /// <summary>
        /// 初始化 <see cref="UnknownChatMessage"/> 类的新实例
        /// </summary>
        /// <param name="data">服务器响应的内容</param>
        [Obsolete("本类不应由用户进行实例化。")]
        public UnknownChatMessage(JsonElement data)
        {
            Rawdata = data;
        }
    }
}
