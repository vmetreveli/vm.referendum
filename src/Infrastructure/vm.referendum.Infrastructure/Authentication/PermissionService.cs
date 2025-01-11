using vm.referendum.Domain.Entities;
using vm.referendum.Domain.Entities.User;
using vm.referendum.Infrastructure.Context;

namespace vm.referendum.Infrastructure.Authentication;

public class PermissionService(DataContext context) : IPermissionService
{
    public async Task<HashSet<string>> GetPermissionsAsync(Guid memberId)
    {
        var roles = await context.Set<User>()
            .Include(x => x.Role)
            .ThenInclude(x => x.Permissions)
            .Where(x => x.Id == memberId)
            .Select(x => x.Role)
            .ToArrayAsync();

        return roles
            .Select(x => x)
            .SelectMany(x => x.Permissions)
            .Select(x => x.Name)
            .ToHashSet();
    }
}