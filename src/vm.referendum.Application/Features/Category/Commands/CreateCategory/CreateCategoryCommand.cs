using vm.referendum.Domain.Abstractions;

namespace vm.referendum.Application.Features.Category.Commands.CreateCategory;

public sealed record CreateCategoryCommand(string Name) : ICommand;