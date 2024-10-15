using vm.referendum.Domain.Entities;

namespace vm.referendum.Infrastructure.Context.Configurations;

internal sealed class RoleConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Value);

        builder.HasMany(x => x.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>();

        builder.HasData(Role.GetValues());
    }
}