namespace vm.referendum.Domain.Entities;

public sealed class UserRole : AggregateRoot<Guid>, IAuditableEntity, IDeletableEntity
{
    private UserRole(Guid userId, Guid roleId) : base(Guid.NewGuid())
    {
        UserId = userId;
        RoleId = roleId;
    }

    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }
    public DateTime CreatedOn { get; }
    public DateTime ModifiedOn { get; }
    public bool IsDeleted { get; }
    public DateTime? DeletedOn { get; }

    public static UserRole Create(Guid userId)
    {
        return new UserRole(userId, Role.Basic.Value);
    }
}