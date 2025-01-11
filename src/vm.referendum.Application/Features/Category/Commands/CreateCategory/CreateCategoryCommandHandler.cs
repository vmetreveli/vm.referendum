using Framework.Abstractions.Exceptions;
using vm.referendum.Domain.Repository;
using vm.referendum.Domain.ValueObjects;

namespace vm.referendum.Application.Features.Category.Commands.CreateCategory;

public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCategoryCommand>
{
    public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryName = Name.Create(request.Name);

        var category = Domain.Entities.Category.Category.CreateCategory(categoryName);

        await categoryRepository.AddAsync(category, cancellationToken);

        await unitOfWork.CompleteAsync(cancellationToken);
    }
}