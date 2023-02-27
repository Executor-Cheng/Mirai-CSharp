using System;
using System.Collections;
using System.Collections.Generic;
using Mirai.CSharp.Models.ChatMessages;

namespace Mirai.CSharp.Builders
{
    /// <summary>
    /// 用于构建一个转发消息的接口
    /// </summary>
    public interface IForwardMessageBuilder : IEnumerable<IForwardMessageNode>
    {
        /// <summary>
        /// 已添加的消息节点计数
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 使用已添加的所有消息节点创建一个 <see cref="IForwardMessage"/> 实例
        /// </summary>
        /// <returns>一个 <see cref="IForwardMessage"/> 实例, 可用于消息发送</returns>
        IForwardMessage Build();

        /// <summary>
        /// 为当前的 <see cref="IForwardMessageBuilder"/> 添加一条现有的 <see cref="IForwardMessageNode"/>
        /// </summary>
        /// <param name="node">欲添加的现有 <see cref="IForwardMessageNode"/> 实例</param>
        /// <returns>传入的 <see cref="IForwardMessageBuilder"/> 实例, 可继续用于链式调用</returns>
        IForwardMessageBuilder AddNode(IForwardMessageNode node);

        /// <summary>
        /// 为当前的 <see cref="IForwardMessageBuilder"/> 添加一些现有的 <see cref="IForwardMessageNode"/>
        /// </summary>
        /// <param name="nodes">一个 <see cref="IForwardMessageNode"/> 集合, 其中所有的实例都将添加进本构建器</param>
        /// <returns>传入的 <see cref="IForwardMessageBuilder"/> 实例, 可继续用于链式调用</returns>
        IForwardMessageBuilder AddNodes(IEnumerable<IForwardMessageNode> nodes);

        /// <summary>
        /// 为当前的 <see cref="IForwardMessageBuilder"/> 添加一条相同Id的消息
        /// </summary>
        /// <param name="id">消息Id</param>
        /// <returns>传入的 <see cref="IForwardMessageBuilder"/> 实例, 可继续用于链式调用</returns>
        IForwardMessageBuilder AddNode(int id);

        /// <summary>
        /// 为当前的 <see cref="IForwardMessageBuilder"/> 添加一条消息
        /// </summary>
        /// <param name="name">来源昵称</param>
        /// <param name="qqNumber">来源QQ</param>
        /// <param name="time">消息发送时间</param>
        /// <param name="chainBuilder">消息具体内容构建器</param>
        /// <returns>传入的 <see cref="IForwardMessageBuilder"/> 实例, 可继续用于链式调用</returns>
        IForwardMessageBuilder AddNode(string name, long qqNumber, DateTime time, IMessageChainBuilder chainBuilder);

        /// <summary>
        /// 为当前的 <see cref="IForwardMessageBuilder"/> 添加一条消息
        /// </summary>
        /// <param name="name">来源昵称</param>
        /// <param name="qqNumber">来源QQ</param>
        /// <param name="time">消息发送时间</param>
        /// <param name="messages">一系列的具体消息</param>
        /// <returns>传入的 <see cref="IForwardMessageBuilder"/> 实例, 可继续用于链式调用</returns>
        IForwardMessageBuilder AddNode(string name, long qqNumber, DateTime time, params IChatMessage[] messages);

        /// <summary>
        /// 为当前的 <see cref="IForwardMessageBuilder"/> 添加一条消息
        /// </summary>
        /// <param name="messageId">将引用的消息唯一标识符</param>
        /// <param name="target">将引用的消息上下文目标, 好友QQ号/群号</param>
        /// <returns>传入的 <see cref="IForwardMessageBuilder"/> 实例, 可继续用于链式调用</returns>
        public abstract IForwardMessageBuilder AddNode(int messageId, long target);
    }

    /// <summary>
    /// 用于构建一个转发消息的抽象基类
    /// </summary>
    public abstract class ForwardMessageBuilder : IForwardMessageBuilder
    {
        /// <inheritdoc/>
        public virtual int Count => _nodes.Count;

        protected readonly List<IForwardMessageNode> _nodes = new List<IForwardMessageNode>();

        /// <inheritdoc/>
        public abstract IForwardMessage Build();

        /// <inheritdoc/>
        public virtual IForwardMessageBuilder AddNode(IForwardMessageNode node)
        {
            _nodes.Add(node);
            return this;
        }

        /// <inheritdoc/>
        public virtual IForwardMessageBuilder AddNodes(IEnumerable<IForwardMessageNode> nodes)
        {
            _nodes.AddRange(nodes);
            return this;
        }

        /// <inheritdoc/>
        public virtual IEnumerator<IForwardMessageNode> GetEnumerator()
        {
            return _nodes.GetEnumerator();
        }

        /// <inheritdoc/>
        public abstract IForwardMessageBuilder AddNode(int id);

        /// <inheritdoc/>
        public abstract IForwardMessageBuilder AddNode(string name, long qqNumber, DateTime time, IMessageChainBuilder chainBuilder);

        /// <inheritdoc/>
        public abstract IForwardMessageBuilder AddNode(string name, long qqNumber, DateTime time, params IChatMessage[] messages);

        /// <inheritdoc/>
        public abstract IForwardMessageBuilder AddNode(int messageId, long target);

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
