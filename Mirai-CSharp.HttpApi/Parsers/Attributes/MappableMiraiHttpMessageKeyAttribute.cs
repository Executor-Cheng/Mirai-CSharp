using System;

namespace Mirai.CSharp.HttpApi.Parsers.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public abstract class MappableMiraiHttpMessageKeyBaseAttribute : Attribute
    {
        public string Key { get; }

        protected MappableMiraiHttpMessageKeyBaseAttribute(string key)
        {
            Key = key;
        }
    }

    /// <summary>
    /// 标记一个消息类或者消息接口在 mirai-api-http 协议中对应的type值
    /// </summary>
    public class MappableMiraiHttpMessageKeyAttribute : MappableMiraiHttpMessageKeyBaseAttribute
    {
        public MappableMiraiHttpMessageKeyAttribute(string key) : base(key)
        {
            
        }
    }

    /// <summary>
    /// 标记一个聊天在 mirai-api-http 协议中对应的type值
    /// </summary>
    public class MappableMiraiChatMessageKeyAttribute : MappableMiraiHttpMessageKeyBaseAttribute
    {
        public MappableMiraiChatMessageKeyAttribute(string key) : base(key)
        {

        }
    }
}
