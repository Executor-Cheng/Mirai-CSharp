using System;

namespace Mirai_CSharp.Exceptions
{
    /// <summary>
    /// 当给定的机器人QQ不存在时引发的异常
    /// </summary>
    public sealed class BotNotFoundException : Exception
    {
        /// <summary>
        /// 机器人QQ
        /// </summary>
        public long BotQQ { get; }

        public BotNotFoundException() : this("给定的机器人QQ不存在。")
        {

        }

        public BotNotFoundException(string message) : base(message)
        {

        }

        public BotNotFoundException(long botQQ) : this(botQQ, "给定的机器人QQ不存在。", null)
        {

        }

        public BotNotFoundException(long botQQ, string message) : this(botQQ, message, null)
        {

        }

        public BotNotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public BotNotFoundException(long botQQ, string message, Exception innerException) : base(message, innerException)
        {
            BotQQ = botQQ;
        }
    }
}
