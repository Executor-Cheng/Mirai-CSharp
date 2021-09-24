using Mirai.CSharp.Framework.Parsers;
using Mirai.CSharp.Models.EventArgs;

namespace Mirai.CSharp.Parsers
{
    /// <summary>
    /// 表示用于处理Mirai消息数据的接口
    /// </summary>
    public interface IMiraiMessageParser : IMessageParser
    {

    }

    /// <summary>
    /// 表示用于处理消息数据的接口
    /// </summary>
    /// <typeparam name="TRawdata">原始消息数据类型</typeparam>
    public interface IMiraiMessageParser<TRawdata> : IMiraiMessageParser, IMessageParser<TRawdata>
    {
        
    }

    public abstract class MiraiMessageParser<TRawdata> : MessageParser<TRawdata>, IMiraiMessageParser<TRawdata>
    {

    }

    /// <summary>
    /// 表示用于处理消息数据到特定类型的接口
    /// </summary>
    /// <typeparam name="TRawdata">原始消息数据类型</typeparam>
    /// <typeparam name="TMessage">消息类型</typeparam>
    public interface IMiraiMessageParser<TRawdata, TMessage> : IMessageParser<TRawdata, TMessage>, IMiraiMessageParser<TRawdata> where TMessage : IMiraiMessage<TRawdata>
    {

    }

    public abstract class MiraiMessageParser<TRawdata, TMessage> : MessageParser<TRawdata, TMessage>, IMessageParser<TRawdata, TMessage> where TMessage : IMiraiMessage<TRawdata>
    {
        
    }
}
