using System;

namespace Mirai_CSharp.Exceptions
{
    /// <summary>
    /// 提供错误的AuthKey时引发的异常
    /// </summary>
    public sealed class InvalidAuthKeyException : Exception
    {
        /// <summary>
        /// 错误的AuthKey
        /// </summary>
        public string AuthKey { get; }

        public InvalidAuthKeyException() : this(null, "错误的AuthKey。")
        {

        }

        public InvalidAuthKeyException(string authKey) : this(authKey, "错误的AuthKey。", null)
        {

        }

        public InvalidAuthKeyException(string authKey, string message) : this(authKey, message, null)
        {

        }

        public InvalidAuthKeyException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public InvalidAuthKeyException(string authKey, string message, Exception innerException) : base(message, innerException)
        {
            AuthKey = authKey;
        }
    }
}
