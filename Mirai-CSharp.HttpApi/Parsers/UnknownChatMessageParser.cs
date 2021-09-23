using System.Text.Json;
using Mirai.CSharp.HttpApi.Models.ChatMessages;

namespace Mirai.CSharp.HttpApi.Parsers
{
    public class UnknownChatMessageParser : UnknownChatMessageParser<IUnknownChatMessage, UnknownChatMessage>
    {
        /// <summary>
        /// 初始化 <see cref="UnknownChatMessageParser"/> 类的新实例
        /// </summary>
        public UnknownChatMessageParser()
        {

        }
    }

    public class UnknownChatMessageParser<TMessage, TImpl> : MiraiHttpChatMessageParser<TMessage> where TMessage : IUnknownChatMessage
                                                                                                  where TImpl : UnknownChatMessage, TMessage, new()
    {
        public override bool CanParse(in JsonElement root)
        {
            return true;
        }

        public override TMessage Parse(in JsonElement root)
        {
            TImpl message = new TImpl();
            message.Rawdata = root;
            return message;
        }
    }
}
