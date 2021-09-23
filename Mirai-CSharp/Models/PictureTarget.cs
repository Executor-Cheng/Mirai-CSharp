using System;

namespace Mirai.CSharp.Models
{
    /// <summary>
    /// 图片的目标类型
    /// </summary>
    [Obsolete("请使用 UploadTarget")]
    public enum PictureTarget
    {
        /// <summary>
        /// 好友
        /// </summary>
        Friend,
        /// <summary>
        /// 群
        /// </summary>
        Group,
        /// <summary>
        /// 临时会话
        /// </summary>
        Temp
    }
}
