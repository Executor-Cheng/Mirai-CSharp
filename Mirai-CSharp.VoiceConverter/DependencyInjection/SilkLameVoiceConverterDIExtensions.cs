using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Mirai.CSharp.Services;
using Mirai.CSharp.VoiceConverter;

namespace Mirai_CSharp.VoiceConverter.DependencyInjection
{
    public static class SilkLameVoiceConverterDIExtensions
    {
        public static IServiceCollection AddSilkLameVoiceConverter(this IServiceCollection services)
        {
            services.TryAddSingleton<IVoiceConverter, SilkLameVoiceConverter>();
            services.TryAddSingleton<ISilkLameCoder, SilkLameCoder>();
            return services;
        }
    }
}
