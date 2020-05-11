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
        [DoesNotReturn]
        private static T ThrowCommonException<T>(int code, in JsonElement root)
        {
            switch (code)
            {
                case 1:
                    {
                        throw new ArgumentException("错误的auth key。", nameof(MiraiHttpSessionOptions.AccessKey));
                    }
                case 2:
                    {
                        throw new ArgumentException("指定的Bot不存在。", nameof(QQNumber));
                    }
                case 3:
                    {
                        throw new ArgumentException("Session失效或不存在。", nameof(InternalSessionInfo.SessionKey));
                    }
                case 4:
                    {
                        throw new InvalidOperationException("Session未认证(未激活)。");
                    }
                case 5:
                    {
                        throw new ArgumentException("发送消息目标不存在(指定对象不存在)。", "targetQQ");
                    }
                case 6:
                    {
                        throw new FileNotFoundException("指定的文件不存在。");
                    }
                case 10:
                    {
                        throw new InvalidOperationException("Bot没有对应操作的权限。");
                    }
                case 20:
                    {
                        throw new InvalidOperationException("Bot被禁言。");
                    }
                case 30:
                    {
                        throw new ArgumentException("消息过长。", nameof(PlainMessage.Message));
                    }
                case 400:
                    {
                        throw new ArgumentException("调用http-api失败, 参数错误。");
                    }
                default:
                    {
                        throw new UnknownResponseException(root.GetRawText());
                    }
            }
        }

        private static void CheckArray(IList chain)
        {
            if (chain == null || chain.Count == 0)
            {
                throw new ArgumentException("消息链必须为非空且至少有1条消息。");
            }
        }

        private void CheckDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MiraiHttpSession));
            }
        }

        private void CheckConnected()
        {
            CheckDisposed();
            if (!Connected)
            {
                throw new InvalidOperationException("请先连接到一个Session。");
            }
        }
    }
}
