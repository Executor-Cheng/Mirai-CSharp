using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供好友同步消息的相关信息接口。继承自 <see cref="ICommonMessageEventArgs"/>
    /// </summary>
    public interface IFriendSyncMessageEventArgs : ICommonMessageEventArgs
    {
        /// <summary>
        /// 目标好友信息
        /// </summary>
        IFriendInfo Subject { get; }
    }

    /// <summary>
    /// 提供好友同步消息的相关信息接口。继承自 <see cref="IFriendSyncMessageEventArgs"/> 和 <see cref="ICommonMessageEventArgs{TRawdata}"/>
    /// </summary>
    public interface IFriendSyncMessageEventArgs<TRawdata> : IFriendSyncMessageEventArgs, ICommonMessageEventArgs<TRawdata>
    {

    }
}
