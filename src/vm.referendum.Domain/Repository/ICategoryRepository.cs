using Framework.Abstractions.Repository;
using vm.referendum.Domain.Entities;

namespace vm.referendum.Domain.Repository;

public interface ICategoryRepository : IRepositoryBase<Category, Guid>
{
}