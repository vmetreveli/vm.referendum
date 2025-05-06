using Microsoft.EntityFrameworkCore.Design;

namespace Framework.Infrastructure.Context;

public class BaseDbContextFactory : IDesignTimeDbContextFactory<BaseDbContext>
{
    public BaseDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<BaseDbContext> optionsBuilder = new();
        optionsBuilder
            .UseNpgsql("DefaultConnection")
            .UseSnakeCaseNamingConvention();

        return new BaseDbContext(optionsBuilder.Options);
    }
}