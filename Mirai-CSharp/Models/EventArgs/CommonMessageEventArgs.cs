using Mirai_CSharp.Utility.JsonConverters;
using System.Text.Json.Serialization;

#pragma warning disable CA1819 // Properties should not return arrays
namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供通用消息的相关信息接口
    /// </summary>
    public interface ICommonMessageEventArgs
    {
        /// <summary>
        /// 消息链数组
        /// </summary>
        [JsonConverter(typeof(IMessageBaseArrayConverter))]
        [JsonPropertyName("messageChain")]
        IMessageBase[] Chain { get; }
    }

    public abstract class CommonMessageEventArgs : ICommonMessageEventArgs
    {
        /// <summary>
        /// 消息链数组
        /// </summary>
        [JsonConverter(typeof(IMessageBaseArrayConverter))]
        [JsonPropertyName("messageChain")]
        public IMessageBase[] Chain { get; set; } = null!;

        protected CommonMessageEventArgs() { }

        protected CommonMessageEventArgs(IMessageBase[] chain)
        {
            Chain = chain;
        }
    }
}
