using Framework.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace vm.referendum.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(DependencyInjection).Assembly);
        services.AddFramework(configuration, typeof(DependencyInjection).Assembly);

        return services;
    }
}