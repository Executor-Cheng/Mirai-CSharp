using Mirai.CSharp.HttpApi.Models.ChatMessages;

namespace Mirai.CSharp.HttpApi.Parsers
{
    /// <summary>
    /// 表示处理mirai-api-http消息的 <see cref="IMiraiHttpChatMessageParser{TMessage}"/>
    /// </summary>
    public interface IMiraiHttpChatMessageParser : IMiraiHttpMessageParserBase
    {

    }

    /// <summary>
    /// 表示处理mirai-api-http消息的 <see cref="IMiraiHttpChatMessageParser{TMessage}"/>
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public interface IMiraiHttpChatMessageParser<TMessage> : IMiraiHttpChatMessageParser,
                                                             IMiraiHttpMessageParserBase<TMessage> where TMessage : IChatMessage
    {

    }

    public abstract class MiraiHttpChatMessageParser<TMessage> : MiraiHttpMessageParserBase<TMessage>, IMiraiHttpChatMessageParser<TMessage> where TMessage : IChatMessage
    {

    }
}
