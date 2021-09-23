using System;
using Mirai.CSharp.Builders;

namespace Mirai.CSharp.Session
{
    public abstract partial class MiraiSession : IMiraiSession
    {
        /// <inheritdoc/>
        public abstract IMessageChainBuilder GetMessageChainBuilder();

        public abstract void Dispose(bool disposing);

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
