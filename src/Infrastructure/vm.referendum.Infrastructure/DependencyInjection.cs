using Framework.Abstractions.Repository;
using Framework.Infrastructure.Interceptors;
using Framework.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using vm.referendum.Infrastructure.Context;
using vm.referendum.Infrastructure.ExternalServices;


namespace vm.referendum.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<InsertOutboxMessagesInterceptor>();
        services.AddScoped<UpdateAuditableEntitiesInterceptor>();
        services.AddScoped<UpdateDeletableEntitiesInterceptor>();

        services
            .AddDbContext<DbContext, referendumDbContext>((sp, options) =>
            {
                var outboxMessagesInterceptor = sp.GetService<InsertOutboxMessagesInterceptor>();
                var auditableInterceptor = sp.GetService<UpdateAuditableEntitiesInterceptor>();
                var deletableEntitiesInterceptor = sp.GetService<UpdateDeletableEntitiesInterceptor>();

                options.UseNpgsql(
                        configuration.GetConnectionString("DefaultConnection"))
                    // options =>
                    // {
                    //     options.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                    //     options.MigrationsHistoryTable($"__{nameof(NotificationDbContext)}");
                    //
                    //     options.EnableRetryOnFailure(5);
                    //     options.MinBatchSize(1);
                    // })
                    .UseSnakeCaseNamingConvention()
                    .AddInterceptors(outboxMessagesInterceptor!)
                    .AddInterceptors(auditableInterceptor!)
                    .AddInterceptors(deletableEntitiesInterceptor!)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            });

        //services.AddScoped<IEventRepository, EventRepository>();
        //  services.AddScoped<IEventDictionaryRepository, EventDictionaryRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork<referendumDbContext>>();


        services.AddExternalServices(configuration);
        return services;
    }
}