using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供陌生人同步消息的相关信息接口。继承自 <see cref="ICommonMessageEventArgs"/>
    /// </summary>
    public interface IStrangerSyncMessageEventArgs : ICommonMessageEventArgs
    {
        /// <summary>
        /// 目标陌生人信息
        /// </summary>
        IStrangerInfo Subject { get; }
    }

    /// <summary>
    /// 提供陌生人同步消息的相关信息接口。继承自 <see cref="IStrangerSyncMessageEventArgs"/> 和 <see cref="ICommonMessageEventArgs{TRawdata}"/>
    /// </summary>
    public interface IStrangerSyncMessageEventArgs<TRawdata> : IStrangerSyncMessageEventArgs, ICommonMessageEventArgs<TRawdata>
    {

    }
}
