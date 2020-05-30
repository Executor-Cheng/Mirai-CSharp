using System;

namespace Mirai_CSharp.Exceptions
{
    /// <summary>
    /// 当发送消息的对象(QQ/群号)不存在时引发的异常
    /// </summary>
    public sealed class TargetNotFoundException : Exception
    {
        internal string _message; // 允许更改异常消息, 并且避免重新抛出异常时丢失StackTrace

        public override string Message => _message ?? base.Message;
        /// <summary>
        /// 目标QQ/群号
        /// </summary>
        public long Target { get; }

        public TargetNotFoundException() : this("给定的目标QQ/群号不存在。")
        {

        }

        public TargetNotFoundException(string message) : base(message)
        {
            _message = message;
        }

        public TargetNotFoundException(long target) : this(target, "给定的目标QQ/群号不存在。", null)
        {

        }

        public TargetNotFoundException(long target, string message) : this(target, message, null)
        {
            _message = message;
        }

        public TargetNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
            _message = message;
        }

        public TargetNotFoundException(long target, string message, Exception innerException) : base(message, innerException)
        {
            Target = target;
            _message = message;
        }
    }
}
