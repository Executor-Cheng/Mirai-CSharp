using Mirai.CSharp.Models;
using Mirai.CSharp.Models.ChatMessages;
using System.Linq;

namespace Mirai.CSharp.Extensions
{
    public static class MessageChainExtensions // 其他的我还没想到, 以后再写
    {
        public static string GetPlain(this IChatMessage[] chain)
        {
            return string.Join(null, chain.GetPlains());
        }

        public static string[] GetPlains(this IChatMessage[] chain)
        {
            return chain.OfType<IPlainMessage>().Select(p => p.Message).ToArray();
        }
    }
}
