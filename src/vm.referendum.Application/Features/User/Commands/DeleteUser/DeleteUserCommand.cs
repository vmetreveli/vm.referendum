using vm.referendum.Domain.Abstractions;

namespace vm.referendum.Application.Features.User.Commands.DeleteUser;

/// <summary>
///     Represents the create user command.
/// </summary>
public sealed record DeleteUserCommand(
    string Email
) : ICommand<Result>;