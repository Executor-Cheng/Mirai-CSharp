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
        /// 通过状态码抛出相应的异常
        /// </summary>
        /// <exception cref="InvalidAuthKeyException"/>
        /// <exception cref="BotNotFoundException"/>
        /// <exception cref="InvalidSessionException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <exception cref="FileNotFoundException"/>
        /// <exception cref="PermissionDeniedException"/>
        /// <exception cref="BotMutedException"/>
        /// <exception cref="MessageTooLongException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="UnknownResponseException"/>
#if !NETSTANDARD2_0
        [DoesNotReturn]
#endif
        private static T ThrowCommonException<T>(int code, in JsonElement root)
        {
            switch (code)
            {
                case 1:
                    {
                        throw new InvalidAuthKeyException();
                    }
                case 2:
                    {
                        throw new BotNotFoundException();
                    }
                case 3:
                    {
                        throw new InvalidSessionException();
                    }
                case 4:
                    {
                        throw new InvalidSessionException();
                    }
                case 5:
                    {
                        throw new TargetNotFoundException();
                    }
                case 6:
                    {
                        throw new FileNotFoundException("指定的文件不存在。");
                    }
                case 10:
                    {
                        throw new PermissionDeniedException();
                    }
                case 20:
                    {
                        throw new BotMutedException();
                    }
                case 30:
                    {
                        throw new MessageTooLongException();
                    }
                case 400:
                    {
                        throw new ArgumentException("调用http-api失败, 参数错误, 请到 https://github.com/Executor-Cheng/Mirai-CSharp/issues 下提交issue。");
                    }
                default:
                    {
                        throw new UnknownResponseException(root.GetRawText());
                    }
            }
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
