using Framework.Abstractions.Repository;
using vm.referendum.Domain.Entities;
using vm.referendum.Domain.Entities.UserRole;

namespace vm.referendum.Domain.Repository;

public interface IUserRoleRepository : IRepositoryBase<UserRole, Guid>
{
}