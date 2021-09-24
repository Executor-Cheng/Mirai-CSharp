using System;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedGroupNameChangedEventArgs = Mirai.CSharp.Models.EventArgs.IGroupNameChangedEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供群名称改变相关信息的接口。继承自 <see cref="ISharedGroupNameChangedEventArgs"/> 和 <see cref="IGroupPropertyChangedEventArgs{TRawdata}"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("GroupNameChangeEvent")]
    public interface IGroupNameChangedEventArgs : ISharedGroupNameChangedEventArgs, IGroupPropertyChangedEventArgs<string>
    {

    }

    public class GroupNameChangedEventArgs : GroupPropertyChangedEventArgs<string>, IGroupNameChangedEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupNameChangedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupNameChangedEventArgs(IGroupInfo group, IGroupMemberInfo @operator, string origin, string current) : base(group, @operator, origin, current)
        {

        }
    }
}
