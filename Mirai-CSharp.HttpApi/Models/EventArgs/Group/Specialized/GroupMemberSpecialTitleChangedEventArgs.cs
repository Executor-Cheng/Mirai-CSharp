using System;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedGroupMemberSpecialTitleChangedEventArgs = Mirai.CSharp.Models.EventArgs.IGroupMemberSpecialTitleChangedEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供群头衔改动相关信息的接口。继承自 <see cref="ISharedGroupMemberSpecialTitleChangedEventArgs"/> 和 <see cref="IGroupMemberPropertyChangedEventArgs{TRawdata}"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("MemberSpecialTitleChangeEvent")]
    public interface IGroupMemberSpecialTitleChangedEventArgs : ISharedGroupMemberSpecialTitleChangedEventArgs, IGroupMemberPropertyChangedEventArgs<string>
    {

    }

    public class GroupMemberSpecialTitleChangedEventArgs : GroupMemberPropertyChangedEventArgs<string>, IGroupMemberSpecialTitleChangedEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberSpecialTitleChangedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberSpecialTitleChangedEventArgs(IGroupMemberInfo member, string origin, string current) : base(member, origin, current)
        {

        }
    }
}
