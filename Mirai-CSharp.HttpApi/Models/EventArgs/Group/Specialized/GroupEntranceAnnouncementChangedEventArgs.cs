using System;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedGroupEntranceAnnouncementChangedEventArgs = Mirai.CSharp.Models.EventArgs.IGroupEntranceAnnouncementChangedEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供入群公告改变相关信息的接口。继承自 <see cref="ISharedGroupEntranceAnnouncementChangedEventArgs"/> 和 <see cref="IGroupPropertyChangedEventArgs{TRawdata}"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("GroupEntranceAnnouncementChangeEvent")]
    public interface IGroupEntranceAnnouncementChangedEventArgs : ISharedGroupEntranceAnnouncementChangedEventArgs, IGroupPropertyChangedEventArgs<string>
    {

    }

    public class GroupEntranceAnnouncementChangedEventArgs : GroupPropertyChangedEventArgs<string>, IGroupEntranceAnnouncementChangedEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupEntranceAnnouncementChangedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupEntranceAnnouncementChangedEventArgs(IGroupInfo group, IGroupMemberInfo @operator, string origin, string current) : base(group, @operator, origin, current)
        {

        }
    }
}
