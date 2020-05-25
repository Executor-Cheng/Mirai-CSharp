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
        /// <summary>
        /// 错误的AuthKey
        /// </summary>
        public IMessageBase[] Chain { get; }

        public MessageTooLongException() : this(null, "消息过长。")
        {

        }

        public MessageTooLongException(IMessageBase[] chain) : this(chain, "消息过长。", null)
        {

        }

        public MessageTooLongException(string message) : base(message)
        {

        }

        public MessageTooLongException(IMessageBase[] chain, string message) : this(chain, message, null)
        {

        }

        public MessageTooLongException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public MessageTooLongException(IMessageBase[] chain, string message, Exception innerException) : base(message, innerException)
        {
            Chain = chain;
        }
    }
}
