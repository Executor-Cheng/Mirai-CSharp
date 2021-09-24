using System;
using System.Collections.Generic;
using Mirai.CSharp.HttpApi.Models.ChatMessages;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedGroupMessageEventArgs = Mirai.CSharp.Models.EventArgs.IGroupMessageEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供群消息的相关信息接口。继承自 <see cref="Mirai.CSharp.Models.EventArgs.IGroupMessageEventArgs{TRawdata}"/> 和 <see cref="IGroupMessageBaseEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("GroupMessage")]
    public interface IGroupMessageEventArgs : ISharedGroupMessageEventArgs, IGroupMessageBaseEventArgs
    {

    }

    public class GroupMessageEventArgs : GroupMessageBaseEventArgs, IGroupMessageEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMessageEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMessageEventArgs(IChatMessage[] chain, IGroupMemberInfo sender) : base(chain, sender)
        {
            
        }

        public override string ToString()
            => $"[{Sender.Group.Name}({Sender.Group.Id})] {Sender.Name}({Sender.Id}) -> {string.Join("", (IEnumerable<ChatMessage>)Chain)}";
    }
}
