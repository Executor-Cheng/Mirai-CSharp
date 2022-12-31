using System;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedGroupDisbandEventArgs = Mirai.CSharp.Models.EventArgs.IGroupDisbandEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs.Group
{
    /// <summary>
    /// 提供群被群主解散后bot离开群相关信息的接口。继承自 <see cref="ISharedGroupDisbandEventArgs"/> 和 <see cref="IGroupOperatingEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("BotLeaveEventDisband")]
    public interface IGroupDisbandEventArgs : ISharedGroupDisbandEventArgs, IGroupOperatingEventArgs
    {

    }

    public class GroupDisbandEventArgs : GroupOperatingEventArgs, IGroupDisbandEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupDisbandEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupDisbandEventArgs(IGroupInfo group, IGroupMemberInfo @operator) : base(group, @operator)
        {

        }
    }
}
