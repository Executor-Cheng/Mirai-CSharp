using Mirai_CSharp.Plugin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

#pragma warning disable IDE0038 // 使用模式匹配
namespace Mirai_CSharp
{
    public partial class MiraiHttpSession
    {
        //private static Task<bool> InvokeAsync<TEventArgs>(IPlugin plugin, MiraiHttpSession session, TEventArgs e)
        //{
        //    return plugin is IPlugin<TEventArgs> ? ((IPlugin<TEventArgs>)plugin).HandleEvent(session, e) : Task.FromResult(false);
        //}

        //public static Task<bool> InvokeAsync<TPlugin, TEventArgs>(this IPlugin plugin, MiraiHttpSession session, TEventArgs e) where TPlugin : IPlugin<TEventArgs>
        //{
        //    //return (plugin as TPlugin)?.HandleEvent(session, e) ?? Task.FromResult(false); // 给 TPlugin 上 class 约束没有意义
        //    return plugin is TPlugin ? ((TPlugin)plugin).HandleEvent(session, e) : Task.FromResult(false);
        //}

        private static async Task InvokeAsync<TEventArgs>(IEnumerable<IPlugin> plugins, CommonEventHandler<TEventArgs>? handlers, MiraiHttpSession session, TEventArgs e)
        {
            try
            {
                foreach (IPlugin plugin in plugins)
                {
                    if (plugin is IPlugin<TEventArgs> tPlugin && await tPlugin.HandleEvent(session, e))
                    {
                        return;
                    }
                }
                if (handlers != null)
                {
                    await InvokeAsync(handlers, session, e);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        private static async Task InvokeAsync<TEventArgs>(CommonEventHandler<TEventArgs> handlers, MiraiHttpSession sender, TEventArgs e)
        {
            foreach (CommonEventHandler<TEventArgs> handler in handlers.GetInvocationList())
            {
                if (await handler.Invoke(sender, e))
                {
                    break;
                }
            }
        }
    }
}
