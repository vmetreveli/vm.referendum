using Microsoft.EntityFrameworkCore;
using vm.referendum.Infrastructure.Context;

namespace vm.referendum.Api.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigration(this WebApplication app)
    {
        // Migrate latest database changes during startup
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider
            .GetRequiredService<DataContext>();

        // Here is the migration executed
        dbContext.Database.Migrate();
    }
}