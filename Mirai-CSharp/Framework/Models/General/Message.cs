using System;
using System.Text.Json.Serialization;

namespace Mirai_CSharp.Framework.Models.General
{
    /// <summary>
    /// 消息的基接口
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// 唯一Id
        /// </summary>
        long Id { get; }
        /// <summary>
        /// 消息时间
        /// </summary>
        DateTime Time { get; }
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
        [JsonIgnore]
        TRawdata Rawdata { get; }
    }

    /// <summary>
    /// 实现消息的基本信息的抽象类
    /// </summary>
    public abstract class Message : IMessage
    {
        /// <inheritdoc/>
        public virtual long Id { get; set; }

        /// <inheritdoc/>
        public virtual DateTime Time { get; set; }
    }

    /// <inheritdoc/>
    /// <typeparam name="TRawdata">原始消息数据类型</typeparam>
    public abstract class Message<TRawdata> : Message, IMessage<TRawdata> // 建议传消息只使用接口（方便继承）
    {
        /// <inheritdoc/>
        [JsonIgnore]
        public TRawdata Rawdata { get; set; } = default!; // Parser 应当设定它为 non-default
    }
}
