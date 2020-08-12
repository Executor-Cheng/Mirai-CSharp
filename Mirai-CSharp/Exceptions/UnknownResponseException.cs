using System;

namespace Mirai_CSharp.Exceptions
{
    public sealed class UnknownResponseException : Exception
    {
        private const string DefaultMessage = "未知的服务器返回。";

        public string? Response { get; }

        public UnknownResponseException() { }

        public UnknownResponseException(string? response) : this(response, DefaultMessage, null) { }

        public UnknownResponseException(string? response, string? message) : this(response, message, null) { }

        public UnknownResponseException(string? response, Exception? innerException) : this(response, DefaultMessage, innerException) { }

        public UnknownResponseException(string? response, string? message, Exception? innerException) : base(message ?? DefaultMessage, innerException)
            => Response = response;
    }
}
