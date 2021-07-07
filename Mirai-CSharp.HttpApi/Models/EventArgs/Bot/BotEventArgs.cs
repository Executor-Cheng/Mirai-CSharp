using System;
using System.Text.Json.Serialization;

namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 机器人本身事件接口。仅含QQ号
    /// </summary>
    public interface IBotEventArgs
    {
        /// <summary>
        /// 机器人QQ号
        /// </summary>
        [JsonPropertyName("qq")]
        long QQNumber { get; }
    }

    public class BotEventArgs : IBotOnlineEventArgs, 
                                IBotPositiveOfflineEventArgs,
                                IBotKickedOfflineEventArgs, 
                                IBotDroppedEventArgs, 
                                IBotReloginEventArgs
    {
        /// <summary>
        /// 机器人QQ号
        /// </summary>
        [JsonPropertyName("qq")]
        public long QQNumber { get; set; }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotEventArgs(long qqNumber)
        {
            QQNumber = qqNumber;
        }
    }

    /// <summary>
    /// 提供Bot登录成功信息的接口。继承自 <see cref="IBotEventArgs"/>
    /// </summary>
    public interface IBotOnlineEventArgs : IBotEventArgs
    {

    }

    /// <summary>
    /// 提供Bot主动离线信息的接口。继承自 <see cref="IBotEventArgs"/>
    /// </summary>
    public interface IBotPositiveOfflineEventArgs : IBotEventArgs
    {

    }

    /// <summary>
    /// 提供Bot被挤下线信息的接口。继承自 <see cref="IBotEventArgs"/>
    /// </summary>
    public interface IBotKickedOfflineEventArgs : IBotEventArgs
    {

    }

    /// <summary>
    /// 提供Bot意外断开连接信息的接口。继承自 <see cref="IBotEventArgs"/>
    /// </summary>
    public interface IBotDroppedEventArgs : IBotEventArgs
    {

    }

    /// <summary>
    /// 提供Bot主动重新登录信息的接口。继承自 <see cref="IBotEventArgs"/>
    /// </summary>
    public interface IBotReloginEventArgs : IBotEventArgs
    {

    }
}
