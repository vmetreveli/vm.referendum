using Microsoft.EntityFrameworkCore.Design;

namespace Framework.Infrastructure.Context;

public class BaseDbContextFactory : IDesignTimeDbContextFactory<BaseDbContext>
{
    public BaseDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BaseDbContext>();
        optionsBuilder
            .UseNpgsql("DefaultConnection")
            .UseSnakeCaseNamingConvention();

        return new BaseDbContext(optionsBuilder.Options);
    }
}