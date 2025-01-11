namespace vm.referendum.Application.Features.User.Commands.CreateUser;

/// <summary>
///     Represents the create user command.
/// </summary>
public sealed record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : ICommand<Guid>;