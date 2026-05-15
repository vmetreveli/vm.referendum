using Meadow_Framework.Core.Abstractions.Primitives;

namespace vm.referendum.Domain.Entities.RolePermission;

public sealed class RolePermission : AggregateRoot<Guid>
{
    public RolePermission() : base(Guid.NewGuid())
    {
    }

    public RolePermission(Guid id, Guid roleId, Guid permissionId) : base(id)
    {
        RoleId = roleId;
        PermissionId = permissionId;
    }

    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }
}