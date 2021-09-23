using System.Collections.Generic;

namespace Mirai.CSharp.Framework.Parsers
{
    public interface IMessageParserResolver<TRawdata, out TParserService> where TParserService : IMessageParser
    {
        TParserService? ResolveParser(in TRawdata rawdata);

        IEnumerable<TParserService> ResolveParsers(in TRawdata rawdata);
    }
}
