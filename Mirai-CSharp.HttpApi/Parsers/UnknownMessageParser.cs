using System.Text.Json;
using Mirai.CSharp.HttpApi.Models.EventArgs;

namespace Mirai.CSharp.HttpApi.Parsers
{
    public class UnknownMessageParser : UnknownMessageParser<IUnknownMessageEventArgs, UnknownMessageEventArgs>
    {
        /// <summary>
        /// 初始化 <see cref="UnknownMessageParser"/> 类的新实例
        /// </summary>
        public UnknownMessageParser()
        {

        }
    }

    public class UnknownMessageParser<TMessage, TImpl> : MiraiHttpMessageParser<TMessage> where TMessage : IUnknownMessageEventArgs
                                                                                          where TImpl : UnknownMessageEventArgs, TMessage, new()
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
