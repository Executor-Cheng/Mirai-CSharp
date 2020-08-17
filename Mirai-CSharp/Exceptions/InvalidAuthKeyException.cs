using System;

namespace Mirai_CSharp.Exceptions
{
    /// <summary>
    /// 提供错误的AuthKey时引发的异常
    /// </summary>
    public sealed class InvalidAuthKeyException : Exception
    {
        private const string DefaultMessage = "错误的AuthKey。";

        /// <summary>
        /// 错误的AuthKey
        /// </summary>
        public string? AuthKey { get; }

        public InvalidAuthKeyException() : this(null, DefaultMessage) { }

        public InvalidAuthKeyException(string? authKey) : this(authKey, DefaultMessage, null) { }

        public InvalidAuthKeyException(string? authKey, string? message) : this(authKey, message, null) { }

        public InvalidAuthKeyException(string? message, Exception? innerException) : this(null, message, innerException) { }

        public InvalidAuthKeyException(string? authKey, string? message, Exception? innerException) : base(message ?? DefaultMessage, innerException)
        {
            AuthKey = authKey;
        }
    }
}
