using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace vm.referendum.Infrastructure.Context;

public class referendumDbContext(DbContextOptions<referendumDbContext> options)
    : DbContext(options)
{
    #region Entities

    // public DbSet<EventsDictionary> EventsDictionary { get; set; }
    // public DbSet<Event> Events { get; set; }
    //public DbSet<OutboxMessage> OutboxMessages { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}

public class referendumDbContextFactory : IDesignTimeDbContextFactory<referendumDbContext>
{
    public referendumDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<referendumDbContext>();
        optionsBuilder
            .UseNpgsql("DefaultConnection")
            .UseSnakeCaseNamingConvention();

        return new referendumDbContext(optionsBuilder.Options);
    }
}