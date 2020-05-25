using Mirai_CSharp.Models;
using System.Linq;

namespace Mirai_CSharp.Extensions
{
    public static class MessageChainExtensions // 其他的我还没想到, 以后再写
    {
        public static string GetPlain(this IMessageBase[] chain)
        {
            return string.Join(null, chain.GetPlains());
        }

        public static string[] GetPlains(this IMessageBase[] chain)
        {
            return chain.OfType<PlainMessage>().Select(p => p.Message).ToArray();
        }
    }
}
