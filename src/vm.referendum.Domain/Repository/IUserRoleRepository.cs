using Framework.Abstractions.Repository;
using vm.referendum.Domain.Entities;

namespace vm.referendum.Domain.Repository;

public interface IUserRoleRepository : IRepositoryBase<UserRole, Guid>
{
}