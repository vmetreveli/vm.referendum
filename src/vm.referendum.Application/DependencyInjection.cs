using Framework.Infrastructure;
using Meadow_Framework.Framework.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace vm.referendum.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFramework(configuration, typeof(DependencyInjection).Assembly);
        services.AddAutoMapper(cfg => {}, typeof(DependencyInjection).Assembly);


        //  services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        // services.AddTransient(typeof(IPipelineBehavior<,>),

        return services;
    }
}