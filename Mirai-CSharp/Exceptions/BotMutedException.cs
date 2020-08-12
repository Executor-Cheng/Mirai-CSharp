using System;

namespace Mirai_CSharp.Exceptions
{
    /// <summary>
    /// 尝试使用被禁言的机器人QQ发送消息时引发的异常
    /// </summary>
    public sealed class BotMutedException : Exception
    {
        private const string DefaultMessage = "给定的机器人QQ已被禁言。";

        /// <summary>
        /// 机器人QQ
        /// </summary>
        public long BotQQ { get; }

        public BotMutedException() : this(DefaultMessage) { }

        public BotMutedException(string message) : this(0, message) { }

        public BotMutedException(long botQQ) : this(botQQ, DefaultMessage, null) { }

        public BotMutedException(long botQQ, string message) : this(botQQ, message, null) { }

        public BotMutedException(string message, Exception? innerException) : this(0, message, innerException) { }

        public BotMutedException(long botQQ, string message, Exception? innerException) : base(message ?? DefaultMessage, innerException)
        {
            BotQQ = botQQ;
        }
    }
}
