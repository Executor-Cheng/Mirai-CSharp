using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Mirai.CSharp.Services;

namespace Mirai_CSharp.ImageConverter.DependencyInjection
{
    public static class SkiasharpImageConverterDIExtensions
    {
        public static IServiceCollection AddSkiasharpImageConverter(this IServiceCollection services)
        {
            services.TryAddSingleton<IImageConverter, SkiasharpImageConverter>();
            return services;
        }
    }
}
