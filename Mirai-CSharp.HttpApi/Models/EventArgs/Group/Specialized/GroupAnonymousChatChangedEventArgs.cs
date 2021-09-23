using System;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedGroupAnonymousChatChangedEventArgs = Mirai.CSharp.Models.EventArgs.IGroupAnonymousChatChangedEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供匿名聊天设置被改变相关信息的接口。继承自 <see cref="IGroupPropertyChangedEventArgs{TRawdata}"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("GroupAllowAnonymousChatEvent")]
    public interface IGroupAnonymousChatChangedEventArgs : ISharedGroupAnonymousChatChangedEventArgs, IGroupPropertyChangedEventArgs<bool>
    {

    }

    public class GroupAnonymousChatChangedEventArgs : GroupPropertyChangedEventArgs<bool>, IGroupAnonymousChatChangedEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupAnonymousChatChangedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupAnonymousChatChangedEventArgs(IGroupInfo group, IGroupMemberInfo @operator, bool origin, bool current) : base(group, @operator, origin, current)
        {

        }
    }
}
