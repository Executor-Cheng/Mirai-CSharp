using System;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedGroupMuteAllChangedEventArgs = Mirai.CSharp.Models.EventArgs.IGroupMuteAllChangedEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供全员禁言设置被改变相关信息的接口。继承自 <see cref="ISharedGroupMuteAllChangedEventArgs"/> 和 <see cref="IGroupPropertyChangedEventArgs{TRawdata}"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("GroupMuteAllEvent")]
    public interface IGroupMuteAllChangedEventArgs : ISharedGroupMuteAllChangedEventArgs, IGroupPropertyChangedEventArgs<bool>
    {

    }

    public class GroupMuteAllChangedEventArgs : GroupPropertyChangedEventArgs<bool>, IGroupMuteAllChangedEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMuteAllChangedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMuteAllChangedEventArgs(IGroupInfo group, IGroupMemberInfo @operator, bool origin, bool current) : base(group, @operator, origin, current)
        {

        }
    }
}
