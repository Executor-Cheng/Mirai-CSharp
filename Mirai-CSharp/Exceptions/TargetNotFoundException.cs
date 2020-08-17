using System;

namespace Mirai_CSharp.Exceptions
{
    /// <summary>
    /// 当发送消息的对象(QQ/群号)不存在时引发的异常
    /// </summary>
    public sealed class TargetNotFoundException : Exception
    {
        private const string DefaultMessage = "给定的目标QQ/群号不存在。";

        internal string? _message; // 允许更改异常消息, 并且避免重新抛出异常时丢失StackTrace
        /// <inheritdoc/>
        public override string Message => _message ?? base.Message;
        /// <summary>
        /// 目标QQ/群号
        /// </summary>
        public long Target { get; }

        public TargetNotFoundException() : this(DefaultMessage) { }

        public TargetNotFoundException(string? message) : this(0, message) { }

        public TargetNotFoundException(long target) : this(target, DefaultMessage, null) { }

        public TargetNotFoundException(long target, string? message) : this(target, message, null) { }

        public TargetNotFoundException(string? message, Exception? innerException) : this(0, message, innerException) { }

        public TargetNotFoundException(long target, string? message, Exception? innerException) : base(message ?? DefaultMessage, innerException)
        {
            Target = target;
            _message = message;
        }
    }
}
