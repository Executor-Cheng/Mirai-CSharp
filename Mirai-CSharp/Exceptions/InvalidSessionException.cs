using System;

namespace Mirai_CSharp.Exceptions
{
    /// <summary>
    /// 操作一个失效, 不存在或未认证(未激活)的Session时引发的异常
    /// </summary>
    public sealed class InvalidSessionException : Exception
    {
        public const string DefaultMessage = "尝试操作一个失效, 不存在或未认证(未激活)的Session。";
        /// <summary>
        /// SessionKey
        /// </summary>
        public string? SessionKey { get; }

        public InvalidSessionException() : this(null, DefaultMessage) { }

        public InvalidSessionException(string? sessionKey) : this(sessionKey, DefaultMessage, null) { }

        public InvalidSessionException(string? sessionKey, string? message) : this(sessionKey, message, null) { }

        public InvalidSessionException(string? message, Exception? innerException) : this(null, message, innerException) { }

        public InvalidSessionException(string? sessionKey, string? message, Exception? innerException) : base(message ?? DefaultMessage, innerException)
        {
            SessionKey = sessionKey;
        }
    }
}
