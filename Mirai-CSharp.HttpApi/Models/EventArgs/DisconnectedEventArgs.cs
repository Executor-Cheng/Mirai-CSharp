using System;
using Mirai.CSharp.HttpApi.Models;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供断开连接相关信息的接口
    /// </summary>
    public interface IDisconnectedEventArgs : IMiraiHttpMessage
    {
        /// <summary>
        /// 导致断开连接的异常对象
        /// </summary>
        Exception Exception { get; }

        /// <summary>
        /// 上一次连接到的机器人QQ
        /// </summary>
        long LastConnectedQQNumber { get; }
    }

    public class DisconnectedEventArgs : MiraiHttpMessage, IDisconnectedEventArgs
    {
        /// <inheritdoc/>
        public Exception Exception { get; set; } = null!;

        /// <inheritdoc/>
        public long LastConnectedQQNumber {  get; set; }

        [Obsolete("此类不应由用户主动创建实例。")]
        public DisconnectedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public DisconnectedEventArgs(Exception exception, long lastConnectedQQNumber)
        {
            Exception = exception;
            LastConnectedQQNumber = lastConnectedQQNumber;
        }
    }
}
