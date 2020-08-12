using Mirai_CSharp.Utility.JsonConverters;
using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable CA1819 // Properties should not return arrays
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
    /// <summary>
    /// 表示消息的基本信息
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public class SourceMessage : Messages
    {
        public const string MsgType = "Source";
        /// <summary>
        /// 消息的识别号, 用于引用回复或撤回
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }
        /// <summary>
        /// 消息时间
        /// </summary>
        [JsonConverter(typeof(UnixTimeStampJsonConverter))]
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }

        [Obsolete("此类不应由用户主动创建实例。")]
        public SourceMessage() : base(MsgType) { }

        [Obsolete("此类不应由用户主动创建实例。")]
        public SourceMessage(int id, DateTime time) : this()
        {
            Id = id;
            Time = time;
        }

        public override string ToString()
            => $"[mirai:source:{Id}]";
    }
    /// <summary>
    /// 表示引用消息
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public class QuoteMessage : Messages
    {
        public const string MsgType = "Quote";
        /// <summary>
        /// 被引用回复的原消息的messageId
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }
        /// <summary>
        /// 被引用回复的原消息所接收的群号，当为好友消息时为0
        /// </summary>
        [JsonPropertyName("groupId")]
        public long GroupId { get; set; }
        /// <summary>
        /// 被引用回复的原消息的发送者的QQ号
        /// </summary>
        [JsonPropertyName("senderId")]
        public long SenderId { get; set; }
        /// <summary>
        /// 被引用回复的原消息的接收者者的QQ号（或群号）
        /// </summary>
        [JsonPropertyName("targetId")]
        public long TargetId { get; set; }
        /// <summary>
        /// 被引用回复的原消息的消息链数组
        /// </summary>
        [JsonConverter(typeof(IMessageBaseArrayConverter))]
        [JsonPropertyName("origin")]
        public IMessageBase[] OriginChain { get; set; } = null!; // 只在反序列化时进行赋值, 故不为null

        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public QuoteMessage() : base(MsgType) { }

        public QuoteMessage(int id, long groupId, long senderId, long targetId, IMessageBase[] originChain) : this()
        {
            Id = id;
            GroupId = groupId;
            SenderId = senderId;
            TargetId = targetId;
            OriginChain = originChain;
        }

        public override string ToString()
            => $"[mirai:quote:{Id}]";
    }
    /// <summary>
    /// 表示文字消息
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public class PlainMessage : Messages
    {
        public const string MsgType = "Plain";
        /// <summary>
        /// 文字消息
        /// </summary>
        [JsonPropertyName("text")]
        public string Message { get; set; } = null!;

        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public PlainMessage() : base(MsgType) { }

        public PlainMessage(string message) : this()
        {
            Message = message;
        }

        public override string ToString()
            => Message;
    }
    /// <summary>
    /// 图片消息基类
    /// </summary>
    public abstract class CommonImageMessage : Messages
    {
        /// <summary>
        /// 图片的imageId，群图片与好友图片格式不同。不为空时将忽略url属性
        /// </summary>
        [JsonPropertyName("imageId")]
        public string? ImageId { get; set; }
        /// <summary>
        /// 图片的URL，发送时可作网络图片的链接；接收时为腾讯图片服务器的链接，可用于图片下载
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }
        /// <summary>
        /// 图片的路径，发送本地图片，相对路径于 plugins/MiraiAPIHTTP/images
        /// </summary>
        [JsonPropertyName("path")]
        public string? Path { get; set; }

        protected CommonImageMessage(string type, string? imageId, string? url, string? path) : base(type)
        {
            ImageId = imageId;
            Url = url;
            Path = path;
        }
    }
    /// <summary>
    /// 表示图片消息
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public class ImageMessage : CommonImageMessage
    {
        public const string MsgType = "Image";

        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public ImageMessage() : this(null, null, null) { }

        public ImageMessage(string? imageId, string? url, string? path) : base(MsgType, imageId, url, path)
        {
        }

        public override string ToString()
            => $"[mirai:image:{ImageId}]";
    }
    /// <summary>
    /// 表示闪照消息
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public class FlashImageMessage : CommonImageMessage
    {
        public const string MsgType = "FlashImage";

        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public FlashImageMessage() : this(null, null, null) { }

        public FlashImageMessage(string? imageId, string? url, string? path) : base(MsgType, imageId, url, path)
        {
        }

        public override string ToString()
            => $"[mirai:flashimage:{ImageId}]"; // 不同于源码实现。原同ImageMessage
    }
    /// <summary>
    /// 表示 @特定对象 消息
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public class AtMessage : Messages
    {
        public const string MsgType = "At";
        /// <summary>
        /// 被@的群员QQ号
        /// </summary>
        [JsonPropertyName("target")]
        public long Target { get; set; }
        /// <summary>
        /// At时显示的文字，发送消息时无效，自动使用群名片
        /// </summary>
        [JsonPropertyName("display")]
        public string Display { get; set; } = null!; // 由反序列化赋值

        [Obsolete("请使用AtMessage(long)构造方法初始化本类实例。")]
        public AtMessage() : base(MsgType) { }

        public AtMessage(long target) : this()
        {
            Target = target;
        }

        [Obsolete("请使用AtMessage(long)构造方法初始化本类实例。")]
        public AtMessage(long target, string display) : this(target)
        {
            Display = display;
        }

        public override string ToString()
            => $"[mirai:at:{Target}]";
    }
    /// <summary>
    /// 表示 @全体成员 消息
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public class AtAllMessage : Messages
    {
        public const string MsgType = "AtAll";

        public AtAllMessage() : base(MsgType)
        {

        }

        public override string ToString()
            => "[mirai:atall]";
    }
    /// <summary>
    /// 表示一个QQ表情
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public class FaceMessage : Messages
    {
        public const string MsgType = "Face";
        /// <summary>
        /// QQ表情编号，可选，优先高于name
        /// </summary>
        /// <remarks>
        /// 编号详见 <a href="https://github.com/mamoe/mirai/blob/master/mirai-core/src/commonMain/kotlin/net.mamoe.mirai/message/data/Face.kt#L41"/>
        /// </remarks>
        [JsonPropertyName("faceId")]
        public int Id { get; set; }
        /// <summary>
        /// QQ表情拼音，可选
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public FaceMessage() : base(MsgType) { }

        public FaceMessage(int id, string? name) : this()
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
            => $"[mirai:face:{Id}]";
    }
    /// <summary>
    /// 表示xml消息
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public class XmlMessage : Messages
    {
        public const string MsgType = "Xml";

        [JsonPropertyName("xml")]
        public string Xml { get; set; } = null!;

        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public XmlMessage() : base(MsgType) { }

        public XmlMessage(string xml) : this()
        {
            Xml = xml;
        }

        public override string ToString()
            => $"[mirai:service:60,{Xml}]"; // Xml的ServiceId=60, https://github.com/mamoe/mirai/blob/master/mirai-core/src/commonMain/kotlin/net.mamoe.mirai/message/data/RichMessage.kt#L109
    }
    /// <summary>
    /// 表示Json消息
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public class JsonMessage : Messages
    {
        public const string MsgType = "Json";

        [JsonPropertyName("json")]
        public string Json { get; set; } = null!;

        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public JsonMessage() : base(MsgType) { }

        public JsonMessage(string json) : this()
        {
            Json = json;
        }

        public override string ToString()
            => $"[mirai:service:1,{Json}]"; // Json的ServiceId=1, https://github.com/mamoe/mirai/blob/master/mirai-core/src/commonMain/kotlin/net.mamoe.mirai/message/data/RichMessage.kt#L109
    }
    /// <summary>
    /// 表示App消息
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public class AppMessage : Messages
    {
        public const string MsgType = "App";
        /// <summary>
        /// 消息内容
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; } = null!;

        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public AppMessage() : base(MsgType) { }

        public AppMessage(string content) : this()
        {
            Content = content;
        }

        public override string ToString()
            => $"[mirai:app:{Content}]";
    }
    /// <summary>
    /// 表示戳一戳消息
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public class PokeMessage : Messages
    {
        public const string MsgType = "Poke";
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
        /// <summary>
        /// 戳一戳类型
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("name")]
        public PokeType Name { get; set; }

        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public PokeMessage() : base(MsgType) { }

        public PokeMessage(PokeType name) : this()
        {
            Name = name;
        }

        public override string ToString()
            => $"[mirai:poke:{(int)Name},-1]"; // id在PokeType∈[1,6]时固定为-1
    }

    /// <summary>
    /// 表示语音消息。
    /// </summary>
    /// <remarks>
    /// 提前实现。mirai-api-http不支持传递此消息。
    /// </remarks>
    [Obsolete("mirai-api-http不支持传递此消息。")]
    public class VoiceMessage : Messages
    {
        public const string MsgType = "Voice";
        /// <summary>
        /// 语音文件名
        /// </summary>
        [JsonPropertyName("fileName")]
        public string FileName { get; set; } = null!;
        
        [JsonPropertyName("md5")]
        public string Md5 { get; set; } = null!;
        /// <summary>
        /// 用于下载语音的Url
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = null!;

        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public VoiceMessage() : base(MsgType) { }

        public VoiceMessage(string fileName, string md5, string url) : this()
        {
            FileName = fileName;
            Md5 = md5;
            Url = url;
        }
    }

    /// <summary>
    /// 表示未知消息
    /// </summary>
    public class UnknownMessage : Messages
    {
        public const string MsgType = "Unknown";
        /// <summary>
        /// 消息内容。如有需要请自行解析
        /// </summary>
        public JsonElement Data { get; set; }

        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public UnknownMessage() : base(MsgType) { }

        public UnknownMessage(JsonElement data) : this()
        {
            Data = data;
        }
    }
}
