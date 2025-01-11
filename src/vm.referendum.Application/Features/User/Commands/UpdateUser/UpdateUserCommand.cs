namespace vm.referendum.Application.Features.User.Commands.UpdateUser;

/// <summary>
///     Represents the update user command.
/// </summary>
public sealed record UpdateUserCommand(
    Guid UserId,
    string FirstName,
    string LastName
) : ICommand;