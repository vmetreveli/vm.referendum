using vm.referendum.Domain.Entities;
using vm.referendum.Domain.Entities.Role;
using vm.referendum.Domain.Entities.RolePermission;

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