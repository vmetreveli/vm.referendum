using vm.referendum.Domain.Entities;
using Permission = vm.referendum.Domain.Enums.Permission;

namespace vm.referendum.Infrastructure.Context.Configurations;

internal sealed class RolePermissionConfig : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        builder.HasData(
            // Create(Role.Basic, Permission.ReadMember),
            //  Create(Role.Basic, Permission.UpdateMember)
        );
    }

    private static RolePermission Create(Role role, Permission permission)
    {
        //throw new NotImplementedException();
        return new RolePermission
        {
            RoleId = role.Value,
            PermissionId = (int)permission
        };
    }
}