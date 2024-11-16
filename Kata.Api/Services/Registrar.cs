using Microsoft.Extensions.DependencyInjection;
using PingDong.Kata.Service;

namespace PingDong.Kata.Infrastructure;

public static class Registrar
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IPricingService, PricingService>();

        services.AddSingleton<ICalculateEngine, CalculateEngine>();

        return services;
    }
}
