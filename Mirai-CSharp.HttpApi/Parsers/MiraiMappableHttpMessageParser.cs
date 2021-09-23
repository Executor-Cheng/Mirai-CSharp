using Mirai.CSharp.HttpApi.Models;

namespace Mirai.CSharp.HttpApi.Parsers
{
    /// <summary>
    /// 以 <see cref="string"/> 作为键的 <see cref="IMiraiHttpMessageParser"/>
    /// </summary>
    public interface IMappableMiraiHttpMessageParser : IMappableMiraiHttpMessageParserBase, IMiraiHttpMessageParser
    {

    }

    public interface IMappableMiraiHttpMessageParser<TMessage> : IMappableMiraiHttpMessageParser,
                                                                 IMiraiHttpMessageParser<TMessage>,
                                                                 IMappableMiraiHttpMessageParserBase<TMessage> where TMessage : IMiraiHttpMessage
    {

    }

    /// <summary>
    /// 以 cmd 作为键的 <see cref="IMiraiHttpMessageParser{TMessage}"/>
    /// </summary>
    /// <remarks>
    /// 仅供来自 mirai-api-http 的消息使用
    /// </remarks>
    /// <typeparam name="TMessage"></typeparam>
    public abstract class MappableMiraiHttpMessageParser<TMessage> : MappableMiraiHttpMessageParserBase<TMessage>,
                                                                     IMappableMiraiHttpMessageParser<TMessage> where TMessage : IMiraiHttpMessage
    {
        
    }
}
