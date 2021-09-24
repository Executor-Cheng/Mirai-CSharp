using Mirai.CSharp.Framework.Models.General;

namespace Mirai.CSharp.Framework.Parsers
{
    /// <summary>
    /// 可用特定的 <typeparamref name="TKey"/> 作为键的 <see cref="IMessageParser{TRawdata, TMessage}"/>
    /// </summary>
    /// <remarks>
    /// 本接口仅用于暴露 <see cref="Key"/> 使用, 不约束 TRawdata 和 TMessage
    /// </remarks>
    /// <typeparam name="TKey">要作为键的类型</typeparam>
    public interface IMappableMessageParser<TKey>
    {
        /// <summary>
        /// 用于在外部作为键的属性
        /// </summary>
        TKey Key { get; }
    }

    /// <summary>
    /// 可用特定的 <typeparamref name="TKey"/> 作为键的 <see cref="IMessageParser{TRawdata, TMessage}"/>
    /// </summary>
    /// <typeparam name="TKey">要作为键的类型</typeparam>
    /// <typeparam name="TRawdata">原始消息数据类型</typeparam>
    /// <typeparam name="TMessage">消息类型</typeparam>
    public interface IMappableMessageParser<TKey, TRawdata, TMessage> : IMappableMessageParser<TKey>,
                                                                        IMessageParser<TRawdata, TMessage> where TMessage : IMessage<TRawdata>
    {

    }
}
