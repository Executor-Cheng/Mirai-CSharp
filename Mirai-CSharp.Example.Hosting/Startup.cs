using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mirai.CSharp.Example.Hosting.Handlers;
using Mirai.CSharp.Example.Hosting.Services;
using Mirai.CSharp.HttpApi.Builder;
using Mirai.CSharp.HttpApi.Invoking;
using Mirai.CSharp.HttpApi.Options;
using Mirai.CSharp.HttpApi.Session;

namespace Mirai.CSharp.Example.Hosting
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDefaultMiraiHttpFramework()
                    .ResolveParser<GroupMessageHandler>() // 需要动态添加的 Handler 放这里
                    .ResolveParser<FriendMessageHandler>() // 默认的生命周期是 Singleton
                    .AddInvoker<MiraiHttpMessageHandlerInvoker>() // 自此以下的默认的生命周期是 Scoped
                    .AddHandler<DefaultDisconnectedHandler>(ServiceLifetime.Singleton) // 静态解析的 Handler 放这里。解析时机为 IMiraiHttpSession 被解析时
                    .AddHandler<AutoRejectFriendApplyHandler>()
                    .AddClient<MiraiHttpSession>();

            services.AddSingleton<RobotManager>();

            services.Configure<MiraiHttpSessionOptions>(options =>
            {
                options.Host = "";
                options.Port = 0; // 端口
                options.AuthKey = "0"; // 凭据
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
