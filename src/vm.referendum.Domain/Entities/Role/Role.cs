using System.Collections;
using vm.referendum.Domain.Primitives;

namespace vm.referendum.Domain.Entities.Role;

public sealed class Role : Enumeration<Role>, IEnumerable<Role>
{
    public static readonly Role SuperAdmin = new(new Guid("70195640-f90b-4547-8f73-ee2e2689a5af"), nameof(SuperAdmin));
    public static readonly Role Admin = new(new Guid("307535ba-70b0-4351-b86d-153176d22de7"), nameof(Admin));
    public static readonly Role Moderator = new(new Guid("fc8d42c4-44b7-4b68-a41f-221e71a40fff"), nameof(Moderator));
    public static readonly Role Basic = new(new Guid("e2abc25a-d7c9-4d71-a8d6-9b6a5008d2ed"), nameof(Basic));

    public Role(Guid id, string name) : base(id, name)
    {
    }

    //
    public Role()
    {
    }


    public IEnumerable<Permission.Permission>? Permissions { get; set; }
    public IEnumerable<User.User>? Members { get; set; }


    public IEnumerator<Role> GetEnumerator()
    {
        return (IEnumerator<Role>)new[]
        {
            SuperAdmin, Admin, Moderator, Basic
        }.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}