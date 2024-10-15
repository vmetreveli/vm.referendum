using Microsoft.AspNetCore.Authorization;
using vm.referendum.Domain.Enums;

namespace vm.referendum.Infrastructure.Authentication;

public sealed class HasPermissionAttribute(Permission permission) : AuthorizeAttribute(permission.ToString());