using System;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedGroupConfessTalkChangedEventArgs = Mirai.CSharp.Models.EventArgs.IGroupConfessTalkChangedEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供坦白说设置被改变相关信息的接口。继承自 <see cref="ISharedGroupConfessTalkChangedEventArgs"/> 和 <see cref="IGroupPropertyChangedEventArgs{TRawdata}"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("GroupAllowConfessTalkEvent")]
    public interface IGroupConfessTalkChangedEventArgs : ISharedGroupConfessTalkChangedEventArgs, IGroupPropertyChangedEventArgs<bool>
    {

    }

    public class GroupConfessTalkChangedEventArgs : GroupPropertyChangedEventArgs<bool>, IGroupConfessTalkChangedEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupConfessTalkChangedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupConfessTalkChangedEventArgs(IGroupInfo group, IGroupMemberInfo @operator, bool origin, bool current) : base(group, @operator, origin, current)
        {

        }
    }
}
