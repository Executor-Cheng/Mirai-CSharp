using System;
using System.Text.Json;

namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供断开连接相关信息的接口
    /// </summary>
    public interface IDisconnectedEventArgs
    {
        /// <summary>
        /// 导致断开连接的异常对象
        /// </summary>
        Exception Exception { get; }
    }

    public class DisconnectedEventArgs : IDisconnectedEventArgs
    {
        public Exception Exception { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        public DisconnectedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public DisconnectedEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }
}
