using System;
using System.Text.Json;

#pragma warning disable CS0618 // 此警告是用户专用的
namespace Mirai_CSharp.Models
{
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
        /// <summary>
        /// 初始化 <see cref="UnknownMessage"/> 类的新实例
        /// </summary>
        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public UnknownMessage() : base(MsgType) { }
        /// <summary>
        /// 初始化 <see cref="UnknownMessage"/> 类的新实例
        /// </summary>
        /// <param name="data">服务器响应的内容</param>
        public UnknownMessage(JsonElement data) : this()
        {
            Data = data;
        }
    }
}
