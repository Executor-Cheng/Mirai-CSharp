using System.Runtime.CompilerServices;
using System.Text.Json;
using Mirai.CSharp.HttpApi.Models;
using Mirai.CSharp.Parsers;

namespace Mirai.CSharp.HttpApi.Parsers
{
    /// <summary>
    /// 以 <see cref="string"/> 作为键的 <see cref="IMiraiHttpMessageParser"/>
    /// </summary>
    public interface IMappableMiraiHttpMessageParserBase : IMappableMiraiMessageParser<string>, IMiraiHttpMessageParserBase
    {

    }

    /// <summary>
    /// 以 <see cref="string"/> 作为键的 <see cref="IMappableMiraiHttpMessageParser{TMessage}"/>
    /// </summary>
    /// <typeparam name="TMessage">目标类型</typeparam>
    public interface IMappableMiraiHttpMessageParserBase<TMessage> : IMappableMiraiMessageParser<string, JsonElement, TMessage>,
                                                                     IMiraiHttpMessageParserBase<TMessage>,
                                                                     IMappableMiraiHttpMessageParserBase where TMessage : IMiraiHttpMessage
    {

    }

    /// <summary>
    /// 以 cmd 作为键的 <see cref="IMiraiHttpMessageParser{TMessage}"/>
    /// </summary>
    /// <remarks>
    /// 仅供来自 mirai-api-http 的消息使用
    /// </remarks>
    /// <typeparam name="TMessage"></typeparam>
    public abstract class MappableMiraiHttpMessageParserBase<TMessage> : MiraiHttpMessageParserBase<TMessage>,
                                                                         IMappableMiraiHttpMessageParserBase<TMessage> where TMessage : IMiraiHttpMessage
    {
        /// <summary>
        /// 表示弹幕数据中的 cmd 值
        /// </summary>
        public abstract string Key { get; }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool CanParse(in JsonElement root)
        {
            return root.TryGetProperty("type", out var typeToken) && typeToken.GetString() == Key;
        }
    }
}
