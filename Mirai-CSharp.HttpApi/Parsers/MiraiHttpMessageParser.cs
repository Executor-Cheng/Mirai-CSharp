using Mirai.CSharp.HttpApi.Models;
using Mirai.CSharp.Parsers;

namespace Mirai.CSharp.HttpApi.Parsers
{
    /// <summary>
    /// 表示处理mirai-api-http消息的 <see cref="IMiraiMessageParser{TRawdata, TMessage}"/>
    /// </summary>
    public interface IMiraiHttpMessageParser : IMiraiHttpMessageParserBase
    {

    }

    /// <summary>
    /// 表示处理mirai-api-http消息的 <see cref="IMiraiMessageParser{TRawdata, TMessage}"/>
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public interface IMiraiHttpMessageParser<TMessage> : IMiraiHttpMessageParser,
                                                         IMiraiHttpMessageParserBase<TMessage> where TMessage : IMiraiHttpMessage
    {

    }

    public abstract class MiraiHttpMessageParser<TMessage> : MiraiHttpMessageParserBase<TMessage>, IMiraiHttpMessageParser<TMessage> where TMessage : IMiraiHttpMessage
    {

    }
}
