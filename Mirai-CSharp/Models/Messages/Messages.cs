using System.Text.Json.Serialization;

#pragma warning disable CS0618 // 此警告是用户专用的
namespace Mirai_CSharp.Models
{
    public interface IMessageBase
    {
        /// <summary>
        /// 消息类型。供api或反序列化使用
        /// </summary>
        [JsonPropertyName("type")]
        string Type { get; }
    }

    // -- 构成消息链的类 (为了方便Deserialize, 这些子类都不是readonly的。将来JsonSerializer会像Json.NET一样去寻找对应的有参构造, 届时再改为readonly。) --
    public abstract class Messages : IMessageBase
    {
        /// <summary>
        /// 消息类型。供api或反序列化使用
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; }

        protected Messages(string type)
        {
            Type = type;
        }
    }
}
