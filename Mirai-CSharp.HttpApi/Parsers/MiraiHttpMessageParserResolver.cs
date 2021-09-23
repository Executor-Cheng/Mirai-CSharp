using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Mirai.CSharp.Framework.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Models.EventArgs;
using Mirai.CSharp.Parsers;

namespace Mirai.CSharp.HttpApi.Parsers
{
    public interface IMiraiHttpMessageParserResolverBase<out TParserService> : IMiraiMessageParserResolver<JsonElement, TParserService> where TParserService : IMiraiHttpMessageParserBase
    {

    }

    public abstract class MiraiHttpMessageParserResolverBase<TParserService> : IMiraiHttpMessageParserResolverBase<TParserService> where TParserService : IMiraiHttpMessageParserBase
    {
        private readonly IReadOnlyDictionary<string, IReadOnlyList<TParserService>> _mappedParsers;

        private readonly IEnumerable<TParserService> _nonmappedParsers;

        public IEnumerable<TParserService> UnknownMessageParsers { get; }

        public MiraiHttpMessageParserResolverBase(IEnumerable<TParserService> parsers)
        {
            var mappedParsers = new Dictionary<string, IReadOnlyList<TParserService>>();
            foreach (TParserService parser in parsers)
            {
                if (parser is IMappableMiraiHttpMessageParser mappableParser && parser.GetType().GetCustomAttribute<SuppressAutoMappingAttribute>() == null)
                {
                    string key = mappableParser.Key;
                    if (!mappedParsers.TryGetValue(key, out var list))
                    {
                        mappedParsers[key] = list = new List<TParserService>();
                    }
                    Unsafe.As<IReadOnlyList<TParserService>, List<TParserService>>(ref list).Add(parser);
                }
            }
            _nonmappedParsers = parsers.Except(mappedParsers.Values.SelectMany(p => p));
            UnknownMessageParsers = parsers.Where(p => p is IMiraiHttpMessageParser<IUnknownMessageEventArgs>).ToArray();
            _nonmappedParsers = _nonmappedParsers.Except(UnknownMessageParsers).ToArray();
            _mappedParsers = mappedParsers;
        }

        public TParserService? ResolveParser(in JsonElement rawdata)
        {
            return ResolveParsers(in rawdata).FirstOrDefault();
        }

        public IEnumerable<TParserService> ResolveParsers(in JsonElement rawdata)
        {
            if (rawdata.TryGetProperty("type", out JsonElement typeToken) &&
                _mappedParsers.TryGetValue(typeToken.GetString()!, out var mappedParsers))
            {
                return mappedParsers;
            }
            return _nonmappedParsers;
        }
    }

    public interface IMiraiHttpMessageParserResolver : IMiraiHttpMessageParserResolverBase<IMiraiHttpMessageParser>
    {

    }

    public class MiraiHttpMessageParserResolver : MiraiHttpMessageParserResolverBase<IMiraiHttpMessageParser>, IMiraiHttpMessageParserResolver
    {
        public MiraiHttpMessageParserResolver(IEnumerable<IMiraiHttpMessageParser> parsers) : base(parsers)
        {

        }
    }

    public interface IMiraiHttpChatMessageParserResolver : IMiraiHttpMessageParserResolverBase<IMiraiHttpChatMessageParser>
    {

    }

    public class MiraiHttpChatMessageParserResolver : MiraiHttpMessageParserResolverBase<IMiraiHttpChatMessageParser>, IMiraiHttpChatMessageParserResolver
    {
        public MiraiHttpChatMessageParserResolver(IEnumerable<IMiraiHttpChatMessageParser> parsers) : base(parsers)
        {

        }
    }
}
