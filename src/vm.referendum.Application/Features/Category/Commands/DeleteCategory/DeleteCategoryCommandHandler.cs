using Framework.Infrastructure.Exceptions;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Application.Features.Category.Commands.DeleteCategory;

public sealed class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteCategoryCommand>
{
    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);

        if (category is null)
            throw new ObjectNotFoundException(typeof(Domain.Entities.Category).ToString(),
                request.CategoryId.ToString());

        categoryRepository.Remove(category);
        await unitOfWork.CompleteAsync(cancellationToken);
    }
}