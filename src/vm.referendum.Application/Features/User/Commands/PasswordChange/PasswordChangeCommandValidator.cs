using FluentValidation;

namespace vm.referendum.Application.Features.User.Commands.PasswordChange;

public class PasswordChangeCommandValidator : AbstractValidator<PasswordChangeCommand>
{
    public PasswordChangeCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(x => x.OldPassword)
            .NotEmpty().WithMessage("OldPassword is required.")
            .MinimumLength(6).WithMessage("OldPassword must be at least 6 characters long.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("NewPassword is required.")
            .MinimumLength(6).WithMessage("NewPassword must be at least 6 characters long.");
    }
}