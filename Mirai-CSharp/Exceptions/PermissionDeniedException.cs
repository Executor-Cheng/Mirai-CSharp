using System;

namespace Mirai_CSharp.Exceptions
{
    /// <summary>
    /// 当给定的机器人QQ不具有对应操作的权限时引发的异常
    /// </summary>
    public sealed class PermissionDeniedException : Exception
    {
        private const string DefaultMessage = "给定的机器人QQ不具有对应操作的权限。";

        /// <summary>
        /// 机器人QQ
        /// </summary>
        public long BotQQ { get; }

        public PermissionDeniedException() : this(DefaultMessage) { }

        public PermissionDeniedException(string? message) : this(0, message) { }

        public PermissionDeniedException(long botQQ) : this(botQQ, DefaultMessage, null) { }

        public PermissionDeniedException(long botQQ, string? message) : this(botQQ, message, null) { }

        public PermissionDeniedException(string? message, Exception? innerException) : this(0, message, innerException) { }

        public PermissionDeniedException(long botQQ, string? message, Exception? innerException) : base(message ?? DefaultMessage, innerException)
        {
            BotQQ = botQQ;
        }
    }
}
