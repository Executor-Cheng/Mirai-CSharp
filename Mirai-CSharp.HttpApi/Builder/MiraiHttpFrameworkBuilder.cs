using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Mirai.CSharp.Builders;
using Mirai.CSharp.HttpApi.Handlers;
using Mirai.CSharp.HttpApi.Invoking;
using Mirai.CSharp.HttpApi.Invoking.Attributes;
using Mirai.CSharp.HttpApi.JsonServices;
using Mirai.CSharp.HttpApi.Models.ChatMessages;
using Mirai.CSharp.HttpApi.Parsers;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Session;
using Mirai.CSharp.HttpApi.Utility;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;

namespace Mirai.CSharp.HttpApi.Builder
{
    public interface IMiraiHttpFrameworkBuilder<TChatParserService> : IMiraiFrameworkBuilder<IMiraiHttpMessageHandlerInvoker, IMiraiHttpSession, IMiraiHttpMessageHandler, IMiraiHttpMessageParser> where TChatParserService : IMiraiHttpChatMessageParser
    {
        IMiraiHttpFrameworkBuilder<TChatParserService> AddChatParser<TChatParser>() where TChatParser : class, TChatParserService;

        IMiraiHttpFrameworkBuilder<TChatParserService> AddChatParser<TChatParser>(ServiceLifetime lifetime) where TChatParser : class, TChatParserService;

        IMiraiHttpFrameworkBuilder<TChatParserService> AddChatParser(TChatParserService parserInstance);

        IMiraiHttpFrameworkBuilder<TChatParserService> AddChatParser<TChatParser>(Func<IServiceProvider, TChatParser> factory) where TChatParser : class, TChatParserService;

        IMiraiHttpFrameworkBuilder<TChatParserService> AddChatParser<TChatParser>(Func<IServiceProvider, TChatParser> factory, ServiceLifetime lifetime) where TChatParser : class, TChatParserService;
    }

    public class MiraiHttpFrameworkBuilder<TChatParserService> : MiraiFrameworkBuilder<IMiraiHttpMessageHandlerInvoker, IMiraiHttpSession, IMiraiHttpMessageHandler, IMiraiHttpMessageParser>,
                                                                 IMiraiHttpFrameworkBuilder<TChatParserService> where TChatParserService : IMiraiHttpChatMessageParser
    {
        protected virtual ServiceLifetime DefaultChatParserLifetime => ServiceLifetime.Singleton;

        public MiraiHttpFrameworkBuilder(IServiceCollection services) : base(services)
        {

        }

        public virtual IMiraiHttpFrameworkBuilder<TChatParserService> AddChatParser<TChatParser>() where TChatParser : class, TChatParserService
        {
            return AddChatParser<TChatParser>(DefaultChatParserLifetime);
        }

        public virtual IMiraiHttpFrameworkBuilder<TChatParserService> AddChatParser<TChatParser>(ServiceLifetime lifetime) where TChatParser : class, TChatParserService
        {
            TryAddService(typeof(IMiraiHttpChatMessageParser), typeof(TChatParser), lifetime, out _);
            return this;
        }

        public virtual IMiraiHttpFrameworkBuilder<TChatParserService> AddChatParser(TChatParserService parserInstance)
        {
            TryAddService(typeof(IMiraiHttpChatMessageParser), parserInstance, out _);
            return this;
        }

        public virtual IMiraiHttpFrameworkBuilder<TChatParserService> AddChatParser<TChatParser>(Func<IServiceProvider, TChatParser> factory) where TChatParser : class, TChatParserService
        {
            return AddChatParser(factory, DefaultChatParserLifetime);
        }

        public virtual IMiraiHttpFrameworkBuilder<TChatParserService> AddChatParser<TChatParser>(Func<IServiceProvider, TChatParser> factory, ServiceLifetime lifetime) where TChatParser : class, TChatParserService
        {
            TryAddService(typeof(IMiraiHttpChatMessageParser), factory, lifetime, out _);
            return this;
        }

        protected override void ResolveMessageParser(Type handlerType, ServiceLifetime? lifetime)
        {
            foreach (RegisterMiraiHttpParserAttribute attribute in handlerType.GetCustomAttributes<RegisterMiraiHttpParserAttribute>(false))
            {
                ServiceLifetime parserLifetime = lifetime ?? attribute.Lifetime ?? DefaultParserLifetime;
                AddMessageParser(attribute.ServiceType, attribute.ImplementationType, parserLifetime);
            }
        }

        protected override void ResolveMessageSubscription(Type invokerType, ServiceLifetime? lifetime)
        {
            foreach (RegisterMiraiHttpMessageSubscriptionAttribute attribute in invokerType.GetCustomAttributes<RegisterMiraiHttpMessageSubscriptionAttribute>(false))
            {
                TryAddService(attribute.ServiceType, attribute.ImplementationType, lifetime ?? attribute.Lifetime ?? DefaultSubscriptionLifetime, out _);
            }
        }

        protected override void ResolveMessageParserResolver(Type invokerType, ServiceLifetime? lifetime)
        {
            foreach (RegisterMiraiHttpMessageParserResolverAttribute attribute in invokerType.GetCustomAttributes<RegisterMiraiHttpMessageParserResolverAttribute>(false))
            {
                ServiceLifetime parserResolverLifetime = lifetime ?? attribute.Lifetime ?? DefaultParserResolverLifetime;
                AddMessageParserResolver(attribute.ServiceType, attribute.ImplementationType, parserResolverLifetime);
            }
            foreach (RegisterMiraiHttpChatParserResolverAttribute attribute in invokerType.GetCustomAttributes<RegisterMiraiHttpChatParserResolverAttribute>(false))
            {
                ServiceLifetime parserResolverLifetime = lifetime ?? attribute.Lifetime ?? DefaultParserResolverLifetime;
                AddMessageParserResolver(attribute.ServiceType, attribute.ImplementationType, parserResolverLifetime);
            }
        }

        protected override void ResolveMessageSubscriptionResolver(Type invokerType, ServiceLifetime? lifetime)
        {
            foreach (RegisterMiraiHttpMessageSubscriptionResolverAttribute attribute in invokerType.GetCustomAttributes<RegisterMiraiHttpMessageSubscriptionResolverAttribute>(false))
            {
                TryAddService(attribute.ServiceType, attribute.ImplementationType, lifetime ?? attribute.Lifetime ?? DefaultParserResolverLifetime, out _);
            }
        }
    }

    public class MiraiHttpFrameworkBuilder : MiraiHttpFrameworkBuilder<IMiraiHttpChatMessageParser>
    {
        public MiraiHttpFrameworkBuilder(IServiceCollection services) : base(services)
        {

        }

        public virtual MiraiHttpFrameworkBuilder AddDefaultParsers()
        {
            AddParser<UnknownMessageParser>();
            return this;
        }

        public virtual MiraiHttpFrameworkBuilder AddDefaultChatParsers()
        {
            TryAddService(typeof(IMiraiHttpMessageJsonOptionsFactory), typeof(MiraiHttpMessageJsonOptionsFactory), ServiceLifetime.Singleton, out _); // 解析消息链所必须的服务
            TryAddService(typeof(IMiraiHttpMessageJsonOptions<>), typeof(MiraiHttpMessageJsonOptions<>), ServiceLifetime.Singleton, out _); // 同上
            AddChatParser<DefaultMappableMiraiHttpChatMessageParser<IAppMessage, AppMessage>>().
            AddChatParser<DefaultMappableMiraiHttpChatMessageParser<IAtAllMessage, AtAllMessage>>().
            AddChatParser<DefaultMappableMiraiHttpChatMessageParser<IAtMessage, AtMessage>>().
            AddChatParser<DefaultMappableMiraiHttpChatMessageParser<IFaceMessage, FaceMessage>>().
            AddChatParser<DefaultMappableMiraiHttpChatMessageParser<IFlashImageMessage, FlashImageMessage>>().
            AddChatParser<DefaultMappableMiraiHttpChatMessageParser<IImageMessage, ImageMessage>>().
            AddChatParser<DefaultMappableMiraiHttpChatMessageParser<IJsonMessage, JsonMessage>>().
            AddChatParser<DefaultMappableMiraiHttpChatMessageParser<IPlainMessage, PlainMessage>>().
            AddChatParser<DefaultMappableMiraiHttpChatMessageParser<IPokeMessage, PokeMessage>>().
            AddChatParser<DefaultMappableMiraiHttpChatMessageParser<IQuoteMessage, QuoteMessage>>().
            AddChatParser<DefaultMappableMiraiHttpChatMessageParser<ISourceMessage, SourceMessage>>().
            AddChatParser<DefaultMappableMiraiHttpChatMessageParser<IVoiceMessage, VoiceMessage>>().
            AddChatParser<DefaultMappableMiraiHttpChatMessageParser<IXmlMessage, XmlMessage>>().
            AddChatParser<DefaultMappableMiraiHttpChatMessageParser<IForwardMessage, ForwardMessage>>().
            AddChatParser<DefaultMappableMiraiHttpChatMessageParser<IDiceMessage, DiceMessage>>().
            AddChatParser<DefaultMappableMiraiHttpChatMessageParser<ISharedMusicMessage, SharedMusicMessage>>().
            AddChatParser<DefaultMappableMiraiHttpChatMessageParser<IMarketFaceMessage, MarketFaceMessage>>().
            AddChatParser<UnknownChatMessageParser>();
            return this;
        }
    }

    public static class MiraiHttpFrameworkServiceCollectionExtensions
    {
        public static MiraiHttpFrameworkBuilder AddMiraiHttpFramework(this IServiceCollection services)
        {
            return new MiraiHttpFrameworkBuilder(services);
        }

        public static MiraiHttpFrameworkBuilder AddDefaultMiraiHttpFramework(this IServiceCollection services)
        {
            var builder = new MiraiHttpFrameworkBuilder(services);
            builder.AddDefaultServices();
            return builder;
        }
    }

    public static class MiraiHttpFrameworkBuilderExtensions
    {
        public static MiraiHttpFrameworkBuilder AddDefaultServices(this MiraiHttpFrameworkBuilder builder)
        {
            builder.Services.TryAddSingleton<ChatMessageJsonConverter>();
            builder.AddInvoker<MiraiHttpMessageHandlerInvoker>();
            builder.AddDefaultParsers();
            builder.AddDefaultChatParsers();
            return builder;
        }
    }
}
