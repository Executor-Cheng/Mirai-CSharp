using System.Collections.Generic;
using Mirai.CSharp.Framework.Parsers;

namespace Mirai.CSharp.Parsers
{
    public interface IMiraiMessageParserResolver<TRawdata, out TParserService> : IMessageParserResolver<TRawdata, TParserService> where TParserService : IMiraiMessageParser
    {
        IEnumerable<TParserService> UnknownMessageParsers { get; }
    }
}
