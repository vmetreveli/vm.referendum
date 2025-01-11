namespace vm.referendum.Application.Features.User.Commands.PasswordChange;

public sealed class PasswordChangeCommand : ICommand
{
    public Guid UserId { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}