using Mirai.CSharp.Framework.Parsers;
using Mirai.CSharp.Models.EventArgs;

namespace Mirai.CSharp.Parsers
{
    /// <summary>
    /// 可用特定的 <typeparamref name="TKey"/> 作为键的 <see cref="IMessageParser{TRawdata, TMessage}"/>
    /// </summary>
    /// <remarks>
    /// 本接口仅用于暴露 <see cref="IMappableMessageParser{TKey}.Key"/> 使用, 不约束 TRawdata 和 TMessage
    /// </remarks>
    /// <typeparam name="TKey">要作为键的类型</typeparam>
    public interface IMappableMiraiMessageParser<TKey> : IMappableMessageParser<TKey>
    {
        
    }

    /// <summary>
    /// 可用特定的 <typeparamref name="TKey"/> 作为键的 <see cref="IMessageParser{TRawdata, TMessage}"/>
    /// </summary>
    /// <typeparam name="TKey">要作为键的类型</typeparam>
    /// <typeparam name="TRawdata">原始消息数据类型</typeparam>
    /// <typeparam name="TMessage">消息类型</typeparam>
    public interface IMappableMiraiMessageParser<TKey, TRawdata, TMessage> : IMappableMiraiMessageParser<TKey>,
                                                                             IMiraiMessageParser<TRawdata, TMessage> where TMessage : IMiraiMessage<TRawdata>
    {

    }
}
