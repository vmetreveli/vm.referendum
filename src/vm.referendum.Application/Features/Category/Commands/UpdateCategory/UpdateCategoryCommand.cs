using vm.referendum.Domain.Abstractions;

namespace vm.referendum.Application.Features.Category.Commands.UpdateCategory;

public sealed class UpdateCategoryCommand : ICommand
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
}