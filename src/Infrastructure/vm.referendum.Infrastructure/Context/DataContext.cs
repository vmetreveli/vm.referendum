using System.Reflection;
using Framework.Abstractions;
using vm.referendum.Domain.Entities;

namespace vm.referendum.Infrastructure.Context;

public sealed class DataContext(DbContextOptions options) : DbContext(options),IDbContext
{
    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }


    #region Entites

    public DbSet<Question> Questions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }

    #endregion
}