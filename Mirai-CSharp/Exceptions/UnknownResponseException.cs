using System;
using System.Text.Json;

namespace Mirai_CSharp.Exceptions
{
    public sealed class UnknownResponseException : Exception
    {
        private const string DefaultMessage = "未知的服务器返回。";

        public string? Response { get; }

        public UnknownResponseException() { }

        public UnknownResponseException(in JsonElement root) : this(root.GetRawText()) { }

        public UnknownResponseException(in JsonElement root, string? message) : this(root.GetRawText(), message) { }

        public UnknownResponseException(in JsonElement root, Exception? innerException) : this(root.GetRawText(), innerException) { }

        public UnknownResponseException(in JsonElement root, string? message, Exception? innerException) : this(root.GetRawText(), message, innerException) { }

        public UnknownResponseException(string? response) : this(response, DefaultMessage, null) { }

        public UnknownResponseException(string? response, string? message) : this(response, message, null) { }

        public UnknownResponseException(string? response, Exception? innerException) : this(response, DefaultMessage, innerException) { }

        public UnknownResponseException(string? response, string? message, Exception? innerException) : base(message ?? DefaultMessage, innerException)
            => Response = response;
    }
}
