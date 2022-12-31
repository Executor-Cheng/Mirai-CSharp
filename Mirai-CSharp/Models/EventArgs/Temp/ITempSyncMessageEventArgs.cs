using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供临时同步消息的相关信息接口。继承自 <see cref="ICommonMessageEventArgs"/>
    /// </summary>
    public interface ITempSyncMessageEventArgs : ICommonMessageEventArgs
    {
        /// <summary>
        /// 目标群成员信息
        /// </summary>
        IGroupMemberInfo Subject { get; }
    }

    /// <summary>
    /// 提供临时同步消息的相关信息接口。继承自 <see cref="ITempSyncMessageEventArgs"/> 和 <see cref="IGroupMessageBaseEventArgs{TRawdata}"/>
    /// </summary>
    public interface ITempSyncMessageEventArgs<TRawdata> : ITempSyncMessageEventArgs, ICommonMessageEventArgs<TRawdata>
    {

    }
}
