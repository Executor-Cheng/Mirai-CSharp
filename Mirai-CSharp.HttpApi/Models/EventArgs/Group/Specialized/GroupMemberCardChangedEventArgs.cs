using System;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedGroupMemberCardChangedEventArgs = Mirai.CSharp.Models.EventArgs.IGroupMemberCardChangedEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供群名片改动相关信息的接口。继承自 <see cref="ISharedGroupMemberCardChangedEventArgs"/> 和 <see cref="IGroupMemberPropertyChangedEventArgs{TRawdata}"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("MemberCardChangeEvent")]
    public interface IGroupMemberCardChangedEventArgs : ISharedGroupMemberCardChangedEventArgs, IGroupMemberPropertyChangedEventArgs<string>
    {

    }

    public class GroupMemberCardChangedEventArgs : GroupMemberPropertyChangedEventArgs<string>, IGroupMemberCardChangedEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberCardChangedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberCardChangedEventArgs(IGroupMemberInfo member, string origin, string current) : base(member, origin, current)
        {

        }
    }
}
