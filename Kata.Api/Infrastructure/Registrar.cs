using Microsoft.Extensions.DependencyInjection;

namespace PingDong.Kata.Infrastructure;

public static class Registrar
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ISkuRepository, SkuRepository>();
        services.AddSingleton<IRuleRepository, DiscountRuleRepository>();

        return services;
    }
}
