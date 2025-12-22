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
        Email mailResult = Email.Create(request.Email);
        Domain.Entities.User.User? user = await userRepository.GetByEmailAsync(mailResult, cancellationToken);

        if (user is not null)
        {
            string password = passwordGenerator.GeneratePassword();
            string hashPassword = passwordHasher.HashPassword(password);
            user.ChangePassword(password, hashPassword);

            await unitOfWork.CompleteAsync(cancellationToken);
        }

        return "We Send Recovery Instruction to the mail";
    }
}