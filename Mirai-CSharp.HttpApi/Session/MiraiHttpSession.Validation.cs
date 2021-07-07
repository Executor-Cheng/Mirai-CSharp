using Mirai_CSharp.Exceptions;
using Mirai_CSharp.Models;
using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Json;

namespace Mirai_CSharp
{
    public partial class MiraiHttpSession
    {
        /// <summary>
        /// 通过状态码返回相应的异常
        /// </summary>
        /// <returns>
        /// 根据给定的 <paramref name="code"/> 返回下列异常之一:
        /// <list type="bullet">
        /// <item><term><see cref="InvalidAuthKeyException"/></term><description><paramref name="code"/> 为 1</description></item>
        /// <item><term><see cref="BotNotFoundException"/></term><description><paramref name="code"/> 为 2</description></item>
        /// <item><term><see cref="InvalidSessionException"/></term><description><paramref name="code"/> 为 3 或 4</description></item>
        /// <item><term><see cref="TargetNotFoundException"/></term><description><paramref name="code"/> 为 5</description></item>
        /// <item><term><see cref="FileNotFoundException"/></term><description><paramref name="code"/> 为 6</description></item>
        /// <item><term><see cref="PermissionDeniedException"/></term><description><paramref name="code"/> 为 10</description></item>
        /// <item><term><see cref="BotMutedException"/></term><description><paramref name="code"/> 为 20</description></item>
        /// <item><term><see cref="MessageTooLongException"/></term><description><paramref name="code"/> 为 30</description></item>
        /// <item><term><see cref="ArgumentException"/></term><description><paramref name="code"/> 为 400</description></item>
        /// <item><term><see cref="UnknownResponseException"/></term><description>其它情况</description></item>
        /// </list>
        /// </returns>
        internal static Exception GetCommonException(int code, in JsonElement root)
        {
            return code switch
            {
                1 => new InvalidAuthKeyException(),
                2 => new BotNotFoundException(),
                // 3 or 4 => new InvalidSessionException(), // C# 9.0
                3 => new InvalidSessionException(),
                4 => new InvalidSessionException(),
                5 => new TargetNotFoundException(),
                6 => new FileNotFoundException("指定的文件不存在。"),
                10 => new PermissionDeniedException(),
                20 => new BotMutedException(),
                30 => new MessageTooLongException(),
                400 => new ArgumentException("调用http-api失败, 参数错误, 请到 https://github.com/Executor-Cheng/Mirai-CSharp/issues 下提交issue。"),
                _ => new UnknownResponseException(root.GetRawText())
            };
        }

        private void CheckDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MiraiHttpSession));
            }
        }

        private InternalSessionInfo SafeGetSession()
        {
            CheckDisposed();
            InternalSessionInfo? session = SessionInfo;
            return session ?? throw new InvalidOperationException("请先连接到一个Session。");
        }
    }
}
