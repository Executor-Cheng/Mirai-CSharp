using System;

namespace Mirai_CSharp.Exceptions
{
    /// <summary>
    /// 当给定的机器人QQ不存在时引发的异常
    /// </summary>
    public sealed class BotNotFoundException : Exception
    {
        public const string DefaultMessage = "给定的机器人QQ不存在。";

        /// <summary>
        /// 机器人QQ
        /// </summary>
        public long BotQQ { get; }

        public BotNotFoundException() : this(DefaultMessage) { }

        public BotNotFoundException(string? message) : this(0, message) { }

        public BotNotFoundException(long botQQ) : this(botQQ, DefaultMessage, null) { }

        public BotNotFoundException(long botQQ, string? message) : this(botQQ, message, null) { }

        public BotNotFoundException(string? message, Exception? innerException) : this(0, message, innerException) { }

        public BotNotFoundException(long botQQ, string? message, Exception? innerException) : base(message ?? DefaultMessage, innerException)
        {
            BotQQ = botQQ;
        }
    }
}
