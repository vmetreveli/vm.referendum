using vm.referendum.Domain.Repository;
using vm.referendum.Domain.ValueObjects;

namespace vm.referendum.Application.Features.Category.Commands.UpdateCategory;

public sealed class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCategoryCommand>
{
    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken = default)
    {
        Name getName = Name.Create(request.Name);

        Domain.Entities.Category.Category? category = await categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);

        if (category is null)
            throw new ObjectNotFoundException(typeof(Domain.Entities.Category.Category).ToString(),
                request.CategoryId.ToString());

        category.Update(getName);

        await categoryRepository.AddAsync(category, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
    }
}