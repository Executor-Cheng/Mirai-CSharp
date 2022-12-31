using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供群同步消息相关信息的接口。继承自 <see cref="ICommonMessageEventArgs"/>
    /// </summary>
    public interface IGroupSyncMessageEventArgs : ICommonMessageEventArgs
    {
        /// <summary>
        /// 目标群信息
        /// </summary>
        IGroupInfo Subject { get; }
    }

    /// <summary>
    /// 提供群同步消息相关信息的接口。继承自 <see cref="IGroupSyncMessageEventArgs"/> 和 <see cref="ICommonMessageEventArgs{TRawdata}"/>
    /// </summary>
    public interface IGroupSyncMessageEventArgs<TRawdata> : IGroupSyncMessageEventArgs, ICommonMessageEventArgs<TRawdata>
    {

    }
}
