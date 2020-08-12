using Mirai_CSharp.Models;
using System;

#pragma warning disable CA1819 // Properties should not return arrays
namespace Mirai_CSharp.Exceptions
{
    /// <summary>
    /// 尝试发送过长的消息时引发的异常
    /// </summary>
    public sealed class MessageTooLongException : Exception
    {
        private const string DefaultMessage = "消息过长。";
        /// <summary>
        /// 错误的AuthKey
        /// </summary>
        public IMessageBase[]? Chain { get; }

        public MessageTooLongException() : this(null, DefaultMessage) { }

        public MessageTooLongException(IMessageBase[]? chain) : this(chain, DefaultMessage, null) { }

        public MessageTooLongException(string? message) : this(null, message, null) { }

        public MessageTooLongException(IMessageBase[]? chain, string? message) : this(chain, message, null) { }

        public MessageTooLongException(string? message, Exception? innerException) : this(null, message, innerException) { }

        public MessageTooLongException(IMessageBase[]? chain, string? message, Exception? innerException) : base(message ?? DefaultMessage, innerException)
        {
            Chain = chain;
        }
    }
}
