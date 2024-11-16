using FluentValidation;
using PingDong.Kata.Infrastructure;
using System.Text.Json.Serialization;

namespace PingDong.Kata;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Dependence Services
        services.AddInfrastructure()
                .AddServices()
                // Validation
                .AddValidatorsFromAssemblyContaining<Startup>();

        // WebApi
        services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

                    options.JsonSerializerOptions.Converters.Clear();
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseMiddleware<WebApiMiddleware>()
            .UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
