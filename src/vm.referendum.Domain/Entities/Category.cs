using vm.referendum.Domain.ValueObjects;

namespace vm.referendum.Domain.Entities;

public sealed class Category : AggregateRoot<Guid>, IAuditableEntity, IDeletableEntity
{
    private Category() : base(Guid.NewGuid())
    {
    }

    public Name Name { get; private set; }

    public DateTime CreatedOn { get; }
    public DateTime ModifiedOn { get; }
    public bool IsDeleted { get; }
    public DateTime? DeletedOn { get; }

    public static Category CreateCategory(Name name)
    {
        return new Category
        {
            Name = name
        };
    }

    public void Update(Name name)
    {
        Name = name;
    }
}