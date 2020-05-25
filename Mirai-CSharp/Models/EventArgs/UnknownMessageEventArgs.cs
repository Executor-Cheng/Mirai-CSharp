using System.Text.Json;

namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供未知消息相关信息的接口
    /// </summary>
    public interface IUnknownMessageEventArgs
    {
        /// <summary>
        /// 未知的Json消息。以 <see cref="JsonElement"/> 表示
        /// </summary>
        JsonElement Message { get; }
    }

    public class UnknownMessageEventArgs : IUnknownMessageEventArgs
    {
        public JsonElement Message { get; set; }

        public UnknownMessageEventArgs()
        {

        }

        public UnknownMessageEventArgs(JsonElement message)
        {
            Message = message;
        }
    }
}
