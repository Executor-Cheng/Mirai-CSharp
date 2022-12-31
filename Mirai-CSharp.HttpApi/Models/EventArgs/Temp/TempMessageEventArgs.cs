using System.Collections.Generic;
using Mirai.CSharp.HttpApi.Models.ChatMessages;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedTempMessageEventArgs = Mirai.CSharp.Models.EventArgs.ITempMessageEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供临时消息的相关信息接口。继承自 <see cref="ISharedTempMessageEventArgs"/> 和 <see cref="IGroupMessageBaseEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("TempMessage")]
    public interface ITempMessageEventArgs : ISharedTempMessageEventArgs, IGroupMessageBaseEventArgs
    {

    }

    public class TempMessageEventArgs : GroupMessageBaseEventArgs, ITempMessageEventArgs
    {
        public TempMessageEventArgs()
        {

        }

        public TempMessageEventArgs(IChatMessage[] chain, IGroupMemberInfo sender) : base(chain, sender)
        {

        }

        public override string ToString()
            => $"[{Sender.Group.Name}({Sender.Group.Id})] {Sender.Name}(Temp {Sender.Id}) -> {string.Join("", (IEnumerable<ChatMessage>)Chain)}";
    }
}
