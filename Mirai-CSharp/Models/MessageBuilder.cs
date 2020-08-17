using System.Collections.Generic;

namespace Mirai_CSharp.Models
{
    public interface IMessageBuilder
    {
        int Count { get; }

        IMessageBase[] Build();

        IMessageBuilder Add(IMessageBase message);
    }

    public class MessageBuilder : IMessageBuilder
    {
        protected readonly List<IMessageBase> _list = new List<IMessageBase>();

        public virtual int Count => _list.Count;

        public virtual IMessageBase[] Build()
            => _list.ToArray();

        public IMessageBuilder Add(IMessageBase message)
        {
            _list.Add(message);
            return this;
        }
    }
}
