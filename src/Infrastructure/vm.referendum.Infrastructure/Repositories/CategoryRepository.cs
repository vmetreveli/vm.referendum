using vm.referendum.Domain.Entities.Category;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Infrastructure.Repositories;

public sealed class CategoryRepository(DbContext context)
    : RepositoryBase<DbContext, Category, Guid>(context), ICategoryRepository;