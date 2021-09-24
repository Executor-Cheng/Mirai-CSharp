using System;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Mirai.CSharp.HttpApi.Invoking.Attributes;
using Mirai.CSharp.HttpApi.Parsers;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Session;
using Mirai.CSharp.Invoking;

namespace Mirai.CSharp.HttpApi.Invoking
{
    public interface IMiraiHttpMessageHandlerInvoker : IMiraiMessageHandlerInvoker<IMiraiHttpSession, JsonElement>
    {

    }

    [RegisterMiraiHttpMessageSubscription(typeof(MiraiHttpMessageSubscription<>))]
    [RegisterMiraiHttpMessageSubscriptionResolver(typeof(MiraiHttpMessageSubscriptionResolver))]
    [RegisterMiraiHttpMessageParserResolver(typeof(MiraiHttpMessageParserResolver))]
    [RegisterMiraiHttpChatParserResolver(typeof(MiraiHttpChatMessageParserResolver))]
    public class MiraiHttpMessageHandlerInvoker : MiraiMessageHandlerInvoker<IMiraiHttpSession, JsonElement>, IMiraiHttpMessageHandlerInvoker
    {
        public MiraiHttpMessageHandlerInvoker(IServiceProvider services,
                                              ILogger<MiraiHttpMessageHandlerInvoker> logger,
                                              IMiraiHttpMessageParserResolver parserResolver,
                                              IMiraiHttpMessageSubscriptionResolver subscriptionResolver) : base(services, logger, parserResolver, subscriptionResolver)
        {
            
        }
    }
}
