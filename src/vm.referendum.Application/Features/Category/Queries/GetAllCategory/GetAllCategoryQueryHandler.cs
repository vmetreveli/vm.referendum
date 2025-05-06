using Framework.Infrastructure.Exceptions;
using vm.referendum.Application.Contracts;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Application.Features.Category.Queries.GetAllCategory;

public sealed class GetAllCategoryQueryHandler(ICategoryRepository categoryRepository)
    : IQueryHandler<GetAllCategoryQuery, IReadOnlyList<CategoryResponse>>
{
    public async Task<IReadOnlyList<CategoryResponse>> Handle(GetAllCategoryQuery request,
        CancellationToken cancellationToken)
    {
        IEnumerable<Domain.Entities.Category.Category> result = await categoryRepository.GetAllAsync(cancellationToken);

        if (!result.Any())
            throw new ObjectNotFoundException(typeof(Domain.Entities.Category.Category).ToString(), string.Empty);
        CategoryResponse[] categories = result.Select(i => new CategoryResponse
            {
                Name = i.Name!
            })
            .ToArray();

        return categories;
    }
}