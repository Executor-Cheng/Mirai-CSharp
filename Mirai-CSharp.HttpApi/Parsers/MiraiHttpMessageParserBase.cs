using System.Text.Json;
using Mirai.CSharp.HttpApi.Models;
using Mirai.CSharp.Parsers;

namespace Mirai.CSharp.HttpApi.Parsers
{
    /// <summary>
    /// 表示处理mirai-api-http消息的 <see cref="IMiraiHttpMessageParserBase{TMessage}"/>
    /// </summary>
    public interface IMiraiHttpMessageParserBase : IMiraiMessageParser<JsonElement>
    {

    }

    /// <summary>
    /// 表示处理mirai-api-http消息的 <see cref="IMiraiHttpMessageParserBase{TMessage}"/>
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public interface IMiraiHttpMessageParserBase<TMessage> : IMiraiHttpMessageParserBase,
                                                             IMiraiMessageParser<JsonElement, TMessage> where TMessage : IMiraiHttpMessage
    {

    }

    public abstract class MiraiHttpMessageParserBase<TMessage> : MiraiMessageParser<JsonElement, TMessage>, IMiraiHttpMessageParserBase<TMessage> where TMessage : IMiraiHttpMessage
    {

    }
}
