using vm.referendum.Domain.Abstractions;

namespace vm.referendum.Application.Features.Category.Commands.DeleteCategory;

public sealed class DeleteCategoryCommand : ICommand
{
    public Guid CategoryId { get; set; }
}