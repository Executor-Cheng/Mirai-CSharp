using System;

namespace Mirai.CSharp.HttpApi.Exceptions
{
    /// <summary>
    /// 尚未注册用于处理音频格式所需的编码器时引发的异常
    /// </summary>
    public sealed class MissingVoiceCoderException : Exception
    {
        private const string DefaultMessage = "尚未注册用于处理此音频格式的编码器。";

        public MissingVoiceCoderException() : this(DefaultMessage) { }

        public MissingVoiceCoderException(string? message) : base(message) { }

        public MissingVoiceCoderException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
