using System;
using Mirai.CSharp.Framework.Models.General;

namespace Mirai.CSharp.Framework.Parsers
{
    public interface IMessageParser
    {
        /// <summary>
        /// 本接口将要处理的消息类型
        /// </summary>
        Type MessageType { get; }
    }

    /// <summary>
    /// 表示用于处理消息数据的接口
    /// </summary>
    /// <typeparam name="TRawdata">原始消息数据类型</typeparam>
    public interface IMessageParser<TRawdata> : IMessageParser
    {
        /// <summary>
        /// 测试给定的 <typeparamref name="TRawdata"/> 能否被处理
        /// </summary>
        /// <remarks>
        /// 请确保此方法不抛出任何异常
        /// </remarks>
        /// <param name="root">消息数据</param>
        bool CanParse(in TRawdata root);

        /// <summary>
        /// 将给定的 <typeparamref name="TRawdata"/> 处理为 <see cref="IMessage{TRawdata}"/> 实例
        /// </summary>
        /// <param name="root">消息数据</param>
        IMessage<TRawdata> Parse(in TRawdata root)
#if !NETSTANDARD2_0
            => throw new NotImplementedException()
#endif
            ;
    }

    public abstract class MessageParser<TRawdata> : IMessageParser<TRawdata>
    {
        public abstract Type MessageType { get; }

        public abstract bool CanParse(in TRawdata root);

#if NETSTANDARD2_0
        IMessage<TRawdata> IMessageParser<TRawdata>.Parse(in TRawdata root)
        {
            throw new NotImplementedException();
        }
#endif
    }

    /// <summary>
    /// 表示用于处理消息数据到特定类型的接口
    /// </summary>
    /// <typeparam name="TRawdata">原始消息数据类型</typeparam>
    /// <typeparam name="TMessage">消息类型</typeparam>
    public interface IMessageParser<TRawdata, TMessage> : IMessageParser<TRawdata> where TMessage : IMessage<TRawdata>
    {
#if !NETSTANDARD2_0
        /// <inheritdoc/>
        Type IMessageParser.MessageType => typeof(TMessage);
#endif

        /// <summary>
        /// 将给定的 <typeparamref name="TRawdata"/> 处理为 <typeparamref name="TMessage"/> 实例
        /// </summary>
        /// <param name="root">消息数据</param>
        new TMessage Parse(in TRawdata root);

#if !NETSTANDARD2_0
        /// <inheritdoc/>
        IMessage<TRawdata> IMessageParser<TRawdata>.Parse(in TRawdata root)
        {
            return Parse(in root);
        }
#endif
    }

    public abstract class MessageParser<TRawdata, TMessage> : MessageParser<TRawdata>, IMessageParser<TRawdata, TMessage> where TMessage : IMessage<TRawdata>
    {
        public override Type MessageType => typeof(TMessage);

        public abstract TMessage Parse(in TRawdata root);

        IMessage<TRawdata> IMessageParser<TRawdata>.Parse(in TRawdata root)
        {
            return Parse(in root);
        }
    }
}
