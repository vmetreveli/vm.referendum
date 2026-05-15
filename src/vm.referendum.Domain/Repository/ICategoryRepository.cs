using Meadow_Framework.Core.Abstractions.Repository;
using vm.referendum.Domain.Entities.Category;

namespace vm.referendum.Domain.Repository;

public interface ICategoryRepository : IRepositoryBase<Category, Guid>
{
}