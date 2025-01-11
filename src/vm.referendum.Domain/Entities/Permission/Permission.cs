namespace vm.referendum.Domain.Entities.Permission;

public sealed class Permission : AggregateRoot<int>, IAuditableEntity, IDeletableEntity
{
    private Permission(int id) : base(id)
    {
    }

    public Permission(int id, string name) : base(id)
    {
        Name = name;
    }

    private Permission() : base(1)
    {
    }


    public string Name { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOn { get; set; }
}