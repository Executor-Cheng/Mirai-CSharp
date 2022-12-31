using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供群被群主解散后bot离开群相关信息的接口。继承自 <see cref="IGroupOperatingEventArgs"/>
    /// </summary>
    public interface IGroupDisbandEventArgs : IGroupOperatingEventArgs
    {

    }

    /// <summary>
    /// 提供群被群主解散后bot离开群相关信息的接口。继承自 <see cref="IGroupDisbandEventArgs"/> 和 <see cref="IGroupOperatingEventArgs{TRawdata}"/>
    /// </summary>
    public interface IGroupDisbandEventArgs<TRawdata> : IGroupDisbandEventArgs, IGroupOperatingEventArgs<TRawdata>
    {

    }
}
