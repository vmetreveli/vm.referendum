namespace vm.referendum.Application.Features.User.Commands.PasswordReset;

public sealed class PasswordResetCommand : ICommand<string>
{
    public string? Email { get; set; }
}