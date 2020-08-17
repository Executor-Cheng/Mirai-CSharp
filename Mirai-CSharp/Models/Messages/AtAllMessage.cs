using System.Diagnostics;

#pragma warning disable CS0618 // 此警告是用户专用的
namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 表示 @全体成员 消息
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public class AtAllMessage : Messages
    {
        public const string MsgType = "AtAll";
        /// <summary>
        /// 初始化 <see cref="AtAllMessage"/> 类的新实例
        /// </summary>
        public AtAllMessage() : base(MsgType)
        {

        }
        /// <inheritdoc/>
        public override string ToString()
            => "[mirai:atall]";
    }
}
