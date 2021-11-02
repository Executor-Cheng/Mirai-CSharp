using System;
using System.Text.Json;
#if NET6_0_OR_GREATER
using System.Text.Json.Nodes;
#endif

namespace Mirai.CSharp.HttpApi.Exceptions
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

#if NET6_0_OR_GREATER
        public UnknownResponseException(JsonNode node) : this(node.ToJsonString()) { }

        public UnknownResponseException(JsonNode node, string message) : this(node.ToJsonString(), message) { }
#endif

        public UnknownResponseException(string? response) : this(response, DefaultMessage, null) { }

        public UnknownResponseException(string? response, string? message) : this(response, message, null) { }

        public UnknownResponseException(string? response, Exception? innerException) : this(response, DefaultMessage, innerException) { }

        public UnknownResponseException(string? response, string? message, Exception? innerException) : base(message ?? DefaultMessage, innerException)
            => Response = response;
    }
}
