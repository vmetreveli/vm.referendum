namespace vm.referendum.Domain.Entities;

public sealed class RolePermission : AggregateRoot<Guid>
{
    public RolePermission() : base(Guid.NewGuid())
    {
    }

    public RolePermission(Guid id, Guid roleId, int permissionId) : base(id)
    {
        RoleId = roleId;
        PermissionId = permissionId;
    }

    public Guid RoleId { get; set; }
    public int PermissionId { get; set; }
}