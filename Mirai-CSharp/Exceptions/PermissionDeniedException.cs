using System;

namespace Mirai_CSharp.Exceptions
{
    /// <summary>
    /// 当给定的机器人QQ不具有对应操作的权限时引发的异常
    /// </summary>
    public sealed class PermissionDeniedException : Exception
    {
        /// <summary>
        /// 机器人QQ
        /// </summary>
        public long BotQQ { get; }

        public PermissionDeniedException() : this("给定的机器人QQ不具有对应操作的权限。")
        {

        }

        public PermissionDeniedException(string message) : base(message)
        {

        }

        public PermissionDeniedException(long botQQ) : this(botQQ, "给定的机器人QQ不具有对应操作的权限。", null)
        {

        }

        public PermissionDeniedException(long botQQ, string message) : this(botQQ, message, null)
        {

        }

        public PermissionDeniedException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public PermissionDeniedException(long botQQ, string message, Exception innerException) : base(message, innerException)
        {
            BotQQ = botQQ;
        }
    }
}
