using System;
using System.Collections.Generic;
using Mirai.CSharp.HttpApi.Models.ChatMessages;
using ISharedChatMessage = Mirai.CSharp.Models.ChatMessages.IChatMessage;
using ISharedForwardMessage = Mirai.CSharp.Models.ChatMessages.IForwardMessage;
using ISharedForwardMessageBuilder = Mirai.CSharp.Builders.IForwardMessageBuilder;
using ISharedForwardMessageNode = Mirai.CSharp.Models.ChatMessages.IForwardMessageNode;
using ISharedMessageChainBuilder = Mirai.CSharp.Builders.IMessageChainBuilder;
using SharedForwardMessageBuilder = Mirai.CSharp.Builders.ForwardMessageBuilder;

namespace Mirai.CSharp.HttpApi.Builders
{
    public interface IForwardMessageBuilder : ISharedForwardMessageBuilder
    {

    }

    public class ForwardMessageBuilder : SharedForwardMessageBuilder, IForwardMessageBuilder
    {
        public override ISharedForwardMessage Build()
        {
            IForwardMessageNode[] converted = new ForwardMessageNode[_nodes.Count];
            int ix = 0;
            foreach (var node in _nodes)
            {
                converted[ix++] = (IForwardMessageNode)node;
            }
            return new ForwardMessage(converted);
        }

        public override ISharedForwardMessageBuilder AddNode(ISharedForwardMessageNode node)
        {
            if (node is not IForwardMessageNode)
            {
                throw new InvalidOperationException($"添加的消息实例 {node.GetType().FullName} 不实现 {typeof(IForwardMessageNode).FullName}");
            }
            return base.AddNode(node);
        }

        public override ISharedForwardMessageBuilder AddNodes(IEnumerable<ISharedForwardMessageNode> nodes)
        {
            foreach (ISharedForwardMessageNode node in nodes)
            {
                if (node is not IForwardMessageNode)
                {
                    throw new InvalidOperationException($"添加的消息实例 {node.GetType().FullName} 不实现 {typeof(IChatMessage).FullName}");
                }
            }
            return base.AddNodes(nodes);
        }

        public override ISharedForwardMessageBuilder AddNode(int id)
        {
            _nodes.Add(new ForwardMessageNode(id));
            return this;
        }

        public override ISharedForwardMessageBuilder AddNode(string name, long qqNumber, DateTime time, ISharedMessageChainBuilder chainBuilder)
        {
            return AddNode(name, qqNumber, time, chainBuilder.Build());
        }

        public override ISharedForwardMessageBuilder AddNode(string name, long qqNumber, DateTime time, params ISharedChatMessage[] messages)
        {
            if (messages is not IChatMessage[] converted)
            {
                converted = new IChatMessage[messages.Length];
                int ix = 0;
                foreach (var message in messages)
                {
                    if (message is not IChatMessage chatMessage)
                    {
                        throw new InvalidOperationException($"添加的消息实例 {message.GetType().FullName} 不实现 {typeof(IChatMessage).FullName}");
                    }
                    converted[ix++] = chatMessage;
                }
            }
            _nodes.Add(new ForwardMessageNode(name, qqNumber, time, converted));
            return this;
        }

        public override ISharedForwardMessageBuilder AddNode(int messageId, long target)
        {
            _nodes.Add(new ForwardMessageNode(new ForwardMessageNodeReference(messageId, target)));
            return this;
        }
    }

    public class ImmutableForwardMessageBuilder : ForwardMessageBuilder
    {
        protected ISharedForwardMessage? _builtMessage;

        public override ISharedForwardMessage Build()
        {
            if (_builtMessage == null)
            {
                _builtMessage = base.Build();
                _nodes.Clear();
                _nodes.Capacity = 0;
            }
            return _builtMessage;
        }

        public override ISharedForwardMessageBuilder AddNode(ISharedForwardMessageNode node)
        {
            if (_builtMessage != null)
            {
                throw new InvalidOperationException("由于之前进行过构建操作, 无法添加新的消息节点实例");
            }
            return base.AddNode(node);
        }

        public override ISharedForwardMessageBuilder AddNodes(IEnumerable<ISharedForwardMessageNode> nodes)
        {
            if (_builtMessage != null)
            {
                throw new InvalidOperationException("由于之前进行过构建操作, 无法添加新的消息节点实例");
            }
            return base.AddNodes(nodes);
        }
    }
}
