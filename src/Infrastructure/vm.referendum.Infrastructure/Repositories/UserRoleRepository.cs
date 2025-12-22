using vm.referendum.Domain.Entities.UserRole;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Infrastructure.Repositories;

/// <summary>
///     Represents the user repository.
/// </summary>
internal sealed class UserRoleRepository(DbContext dbContext)
    : RepositoryBase<DbContext, UserRole, Guid>(dbContext), IUserRoleRepository
{
}