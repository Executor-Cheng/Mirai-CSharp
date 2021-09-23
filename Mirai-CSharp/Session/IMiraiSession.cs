using Mirai.CSharp.Builders;
using Mirai.CSharp.Framework.Clients;

namespace Mirai.CSharp.Session
{
    public partial interface IMiraiSession : IMessageClient
    {
        /// <summary>
        /// 获取适合于当前会话的 <see cref="IMessageChainBuilder"/>
        /// </summary>
        /// <returns>一个与当前会话关联的 <see cref="IMessageChainBuilder"/></returns>
        IMessageChainBuilder GetMessageChainBuilder();
    }
}
