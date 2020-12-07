using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;

#pragma warning disable CA1031 // Do not catch general exception types
#pragma warning disable CA1810 // Initialize reference type static fields inline
namespace Mirai_CSharp.Utility
{
    internal static class ImageHttpListener // 对于 mirai-api-http v1.7.0及以下版本无法发图的临时解决方案
    {
        internal static readonly HttpListener Listener = new HttpListener();

        internal static int Port;

        internal static readonly ConcurrentDictionary<Guid, Stream> Cache = new ConcurrentDictionary<Guid, Stream>();

        static ImageHttpListener()
        {
            for (int port = 1000; port < 65535; port++)
            {
                Listener.Prefixes.Add($"http://127.0.0.1:{port}/");
                try
                {
                    Listener.Start();
                    Port = port;
                    ProcessRequestAsync();
                    return;
                }
                catch (Exception)
                {
                    if (port == 65534)
                    {
                        throw; // 你tm整台机器都没有一个端口可用那可真是见鬼了
                    }
                    Listener.Prefixes.Clear();
                }
            }
        }

        public static void RegisterImage(Guid guid, Stream imgStream)
        {
            if (Port == 0)
            {
                throw new NotSupportedException();
            }
            Cache.TryAdd(guid, imgStream);
        }

        private static async void ProcessRequestAsync()
        {
            while (true)
            {
                try
                {
                    HttpListenerContext ctx = await Listener.GetContextAsync();
                    if (ctx.Request.HttpMethod == "GET" &&
                        ctx.Request.Url!.AbsolutePath == "/fetch" &&
                        Guid.TryParse(ctx.Request.QueryString["guid"], out Guid guid) &&
                        Cache.TryRemove(guid, out Stream? imgStream))
                    {
                        using (imgStream)
                        {
                            ctx.Response.StatusCode = 200;
                            ctx.Response.Headers["Content-Type"] = "application/octet-stream";
                            await imgStream.CopyToAsync(ctx.Response.OutputStream);
                        }
                    }
                    else
                    {
                        ctx.Response.StatusCode = 404;
                    }
                    ctx.Response.Close();
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
