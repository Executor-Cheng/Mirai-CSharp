using System;

namespace Mirai_CSharp.Exceptions
{
    /// <summary>
    /// 操作一个失效, 不存在或未认证(未激活)的Session时引发的异常
    /// </summary>
    public sealed class InvalidSessionException : Exception
    {
        /// <summary>
        /// SessionKey
        /// </summary>
        public string SessionKey { get; }

        public InvalidSessionException() : this(null, "尝试操作一个失效, 不存在或未认证(未激活)的Session。")
        {

        }

        public InvalidSessionException(string sessionKey) : this(sessionKey, "尝试操作一个失效, 不存在或未认证(未激活)的Session。", null)
        {

        }

        public InvalidSessionException(string sessionKey, string message) : this(sessionKey, message, null)
        {

        }

        public InvalidSessionException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public InvalidSessionException(string sessionKey, string message, Exception innerException) : base(message, innerException)
        {
            SessionKey = sessionKey;
        }
    }
}
