using System;
using Mirai.CSharp.Builders;

namespace Mirai.CSharp.Session
{
    public abstract partial class MiraiSession : IMiraiSession
    {
        /// <inheritdoc/>
        public abstract IMessageChainBuilder GetMessageChainBuilder();

        protected virtual void Dispose(bool disposing)
        {
            
        }

        /// <summary>
        /// 释放当前会话所用资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
