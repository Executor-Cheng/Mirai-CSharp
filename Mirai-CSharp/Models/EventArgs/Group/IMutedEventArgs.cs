using System;

namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供禁言时间的接口
    /// </summary>
    public interface IMutedEventArgs
    {
        TimeSpan Duration { get; }
    }
}
