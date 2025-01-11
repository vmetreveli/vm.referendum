using vm.referendum.Domain.Repository;
using vm.referendum.Domain.Services;
using vm.referendum.Domain.ValueObjects;
using vm.referendum.Infrastructure.Authentication.Cryptography;

namespace vm.referendum.Application.Features.User.Commands.PasswordReset;

public class PasswordResetCommandHandler(
    IUserRepository userRepository,
    IEmailService emailService,
    IPasswordGenerator passwordGenerator,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork)
    : ICommandHandler<PasswordResetCommand, string>
{
    private readonly IEmailService _emailService = emailService;


    public async Task<string> Handle(PasswordResetCommand request, CancellationToken cancellationToken)
    {
        var mailResult = Email.Create(request.Email);
        var user = await userRepository.GetByEmailAsync(mailResult, cancellationToken);

        if (user is not null)
        {
            var password = passwordGenerator.GeneratePassword();
            var hashPassword = passwordHasher.HashPassword(password);
            user.ChangePassword(password, hashPassword);

            await unitOfWork.CompleteAsync(cancellationToken);
        }

        return "We Send Recovery Instruction to the mail";
    }
}