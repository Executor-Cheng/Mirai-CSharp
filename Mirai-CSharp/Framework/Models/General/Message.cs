namespace Mirai.CSharp.Framework.Models.General
{
    /// <summary>
    /// 消息的基接口
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// 是否拦截消息传递
        /// </summary>
        bool BlockRemainingHandlers { get; set; }
    }

    /// <summary>
    /// 消息的基本信息接口
    /// </summary>
    /// <typeparam name="TRawdata">原始消息数据类型</typeparam>
    /// <remarks>
    /// 继承自 <see cref="IMessage"/>
    /// </remarks>
    public interface IMessage<TRawdata> : IMessage
    {
        /// <summary>
        /// 原始消息信息
        /// </summary>
        TRawdata Rawdata { get; }
    }

    /// <summary>
    /// 实现消息的基本信息的抽象类
    /// </summary>
    public abstract class Message : IMessage
    {
        /// <inheritdoc/>
        public virtual bool BlockRemainingHandlers { get; set; }
    }

    /// <inheritdoc/>
    /// <typeparam name="TRawdata">原始消息数据类型</typeparam>
    public abstract class Message<TRawdata> : Message, IMessage<TRawdata> // 建议传消息只使用接口（方便继承）
    {
        /// <inheritdoc/>
        public virtual TRawdata Rawdata { get; set; } = default!; // Parser 应当设定它为 non-default
    }
}
