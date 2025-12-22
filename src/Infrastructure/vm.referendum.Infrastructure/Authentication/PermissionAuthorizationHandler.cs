using Microsoft.AspNetCore.Authorization;

namespace vm.referendum.Infrastructure.Authentication;

public class PermissionAuthorizationHandler
    : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        if (context.User.Identity is not { IsAuthenticated: true }) return;
        //   string identityId = context.User.GetIdentityId();

        HashSet<string> permissions = context
            .User
            .Claims
            .Where(i => i.Type == CustomClaims.PERMISSIONS)
            .Select(x => x.Value)
            .ToHashSet();

        if (permissions.Contains(requirement.Permission)) context.Succeed(requirement);
    }
}