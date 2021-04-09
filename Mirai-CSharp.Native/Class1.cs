using System;
using System.Runtime.InteropServices;

namespace Mirai_CSharp.Native
{
    public static class Class1
    {
        [UnmanagedCallersOnlyAttribute(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        public static int Func()
        {
            return 1;
        }
    }
}
