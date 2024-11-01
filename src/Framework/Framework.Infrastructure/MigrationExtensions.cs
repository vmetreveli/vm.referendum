using Framework.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace Framework.Infrastructure;

public static class MigrationExtensions
{
    public static WebApplication ApplyMigration(this WebApplication app)
    {
        var dbContexts = FindDbContexts().ToList(); // Find all event consumers

        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        //  ILogger<Program> logger = services.GetRequiredService<ILogger<Program>>();

        // Get all DbContext types from the assembly
        // var dbContextTypes = Assembly.GetExecutingAssembly().GetTypes()
        //     .Where(t => typeof(DbContext).IsAssignableFrom(t) && !t.IsAbstract);

        foreach (var dbContextType in dbContexts)
        {
            try
            {
                //     logger.LogInformation($"Starting migration for {dbContextType.Name}...");

                // Resolve the DbContext and apply migrations
                var context = (DbContext)services.GetRequiredService(dbContextType);
                context.Database.Migrate();

                //   logger.LogInformation($"Migration completed for {dbContextType.Name}.");
            }
            catch (Exception ex)
            {
                //    logger.LogError(ex, $"An error occurred while migrating the database for {dbContextType.Name}.");
            }
        }

        return app;
    }

    private static IEnumerable<Type> FindDbContexts()
    {
        var consumerInterfaceType = typeof(IDbContext);
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var dbContexts = new List<Type>();

        // Search for classes implementing IDbContext in loaded assemblies
        foreach (var assembly in assemblies)
        {
            dbContexts.AddRange(assembly.GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract)
                .Where(type => type.GetInterfaces().Any(interfaceType => interfaceType == consumerInterfaceType)));
        }

        return dbContexts;
    }
}