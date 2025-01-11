using vm.referendum.Application.Contracts;
using vm.referendum.Domain.Abstractions;

namespace vm.referendum.Application.Features.Category.Queries.GetAllCategory;

public sealed class GetAllCategoryQuery : IQuery<IReadOnlyList<CategoryResponse>>
{
}