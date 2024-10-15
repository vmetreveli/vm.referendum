using vm.referendum.Domain.Abstractions;

namespace vm.referendum.Application.Features.User.Commands.Login;

public sealed record LoginCommand(
    string Email,
    string Password
) : ICommand<string>;