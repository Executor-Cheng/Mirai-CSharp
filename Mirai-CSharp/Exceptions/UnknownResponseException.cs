using System;

namespace Mirai_CSharp.Exceptions
{
    public sealed class UnknownResponseException : Exception
    {
        public string Response { get; }

        public UnknownResponseException() { }

        public UnknownResponseException(string response) : this(response, "未知的服务器返回.", null) { }

        public UnknownResponseException(string response, string message) : this(response, message, null) { }

        public UnknownResponseException(string response, Exception innerException) : this(response, "未知的服务器返回.", innerException) { }

        public UnknownResponseException(string response, string message, Exception innerException) : base(message, innerException)
            => Response = response;
    }
}
