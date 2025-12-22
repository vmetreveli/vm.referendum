using System.Collections;
using vm.referendum.Domain.Primitives;

namespace vm.referendum.Domain.Entities.Permission;

// public sealed class Permission : AggregateRoot<int>, IAuditableEntity, IDeletableEntity
// {
//     private Permission(int id) : base(id)
//     {
//     }
//
//     public Permission(int id, string name) : base(id)
//     {
//         Name = name;
//     }
//
//     private Permission() : base(1)
//     {
//     }
//
//
//     public string Name { get; set; }
//     public DateTime CreatedOn { get; set; }
//     public DateTime ModifiedOn { get; set; }
//     public bool IsDeleted { get; set; }
//     public DateTime? DeletedOn { get; set; }
// }

public sealed class Permission : Enumeration<Permission>
{
    // Predefined Permissions
    public static readonly Permission ReadMember = new(new Guid("80a0d8c0-8a64-426a-9331-71c4bbbe0547"), "ReadMember");

    public static readonly Permission WriteMember = new(new Guid("8f22e919-4c82-4bdc-a04a-b29a901e1f1a"), "WriteMember");

    public static readonly Permission UpdateMember = new(new Guid("bd7f8542-13b7-4e20-8651-d8ea4f25d8a3"), "UpdateMember");

    // Backing collection for enumeration
    private static readonly List<Permission> _allPermissions;

    // Static constructor for thread-safe initialization
    static Permission()
    {
        _allPermissions =
        [
            ReadMember,
            WriteMember,
            UpdateMember
        ];
    }

    public Permission()
    {

    }

    // Constructor
    private Permission(Guid id, string name) : base(id, name)
    {
    }

    // Public static property to access all permissions
    public static IReadOnlyCollection<Permission> AllPermissions => _allPermissions.AsReadOnly();

    // Override ToString for convenience
    public override string ToString()
    {
        return Name;
    }
}


/*
 ReadMember = 1,
 UpdateMember = 2
*/