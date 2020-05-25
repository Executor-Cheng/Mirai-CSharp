using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin
{
    /// <summary>
    /// 表示处理mirai-api-http消息的接口
    /// </summary>
    public interface IPlugin
    {

    }

    /// <summary>
    /// 内部使用。表示处理特定 <typeparamref name="TEventArgs"/> 的接口
    /// </summary>
    public interface IPlugin<TEventArgs> : IPlugin
    {
        /// <summary>
        /// 内部使用
        /// </summary>
        Task<bool> HandleEvent(MiraiHttpSession session, TEventArgs e);
    }
}
