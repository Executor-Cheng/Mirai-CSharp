using Mirai.CSharp.HttpApi.Models.ChatMessages;

namespace Mirai.CSharp.HttpApi.Parsers
{
    /// <summary>
    /// 以 <see cref="string"/> 作为键的 <see cref="IMiraiHttpMessageParser"/>
    /// </summary>
    public interface IMappableMiraiHttpChatMessageParser : IMappableMiraiHttpMessageParserBase, IMiraiHttpChatMessageParser
    {

    }

    public interface IMappableMiraiHttpChatMessageParser<TMessage> : IMappableMiraiHttpChatMessageParser,
                                                                     IMiraiHttpChatMessageParser<TMessage>,
                                                                     IMappableMiraiHttpMessageParserBase<TMessage> where TMessage : IChatMessage
    {

    }

    /// <summary>
    /// 以 cmd 作为键的 <see cref="IMiraiHttpMessageParser{TMessage}"/>
    /// </summary>
    /// <remarks>
    /// 仅供来自 mirai-api-http 的消息使用
    /// </remarks>
    /// <typeparam name="TMessage"></typeparam>
    public abstract class MappableMiraiHttpChatMessageParser<TMessage> : MappableMiraiHttpMessageParserBase<TMessage>,
                                                                         IMappableMiraiHttpChatMessageParser<TMessage> where TMessage : IChatMessage
    {

    }
}
