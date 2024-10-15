namespace vm.referendum.Infrastructure.Authentication;

public interface IPermissionService
{
    Task<HashSet<string>> GetPermissionsAsync(Guid memberId);
}