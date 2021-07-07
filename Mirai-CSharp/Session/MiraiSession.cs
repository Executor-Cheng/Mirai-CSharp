using System;

namespace Mirai_CSharp.Session
{
    public abstract partial class MiraiSession : IMiraiSession
    {
        public abstract void Dispose(bool disposing);

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
