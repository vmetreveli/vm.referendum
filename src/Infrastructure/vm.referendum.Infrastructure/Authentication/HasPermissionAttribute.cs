using Microsoft.AspNetCore.Authorization;
using vm.referendum.Domain.Entities.Permission;
using vm.referendum.Domain.Enums;

namespace vm.referendum.Infrastructure.Authentication;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permissionName)
        : base()
    {
        Policy = $"Permission:{permissionName}";
    }
}