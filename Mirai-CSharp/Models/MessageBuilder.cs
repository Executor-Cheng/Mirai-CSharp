using System.Collections;
using System.Collections.Generic;

namespace Mirai_CSharp.Models
{
    public interface IMessageBuilder : IEnumerable<IMessageBase>
    {
        int Count { get; }

        IMessageBase[] Build();

        IMessageBuilder Add(IMessageBase message);
    }

    public class MessageBuilder : IMessageBuilder, IEnumerable<IMessageBase>
    {
        protected readonly List<IMessageBase> _list = new List<IMessageBase>();

        public MessageBuilder() { }

        public MessageBuilder(IEnumerable<IMessageBase> messages)
            => _list.AddRange(messages);

        public virtual int Count => _list.Count;

        public virtual IMessageBase[] Build()
            => _list.ToArray();

        public IMessageBuilder Add(IMessageBase message)
        {
            _list.Add(message);
            return this;
        }

        IEnumerator<IMessageBase> IEnumerable<IMessageBase>.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
}
