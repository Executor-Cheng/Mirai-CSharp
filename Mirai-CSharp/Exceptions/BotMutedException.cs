using System;

namespace Mirai_CSharp.Exceptions
{
    /// <summary>
    /// 尝试使用被禁言的机器人QQ发送消息时引发的异常
    /// </summary>
    public sealed class BotMutedException : Exception
    {
        /// <summary>
        /// 机器人QQ
        /// </summary>
        public long BotQQ { get; }

        public BotMutedException() : this("给定的机器人QQ已被禁言。")
        {

        }

        public BotMutedException(string message) : base(message)
        {

        }

        public BotMutedException(long botQQ) : this(botQQ, "给定的机器人QQ已被禁言。", null)
        {

        }

        public BotMutedException(long botQQ, string message) : this(botQQ, message, null)
        {

        }

        public BotMutedException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public BotMutedException(long botQQ, string message, Exception innerException) : base(message, innerException)
        {
            BotQQ = botQQ;
        }
    }
}
