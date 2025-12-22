using vm.referendum.Domain.Repository;
using vm.referendum.Infrastructure.Authentication.Cryptography;

namespace vm.referendum.Application.Features.User.Commands.PasswordChange;

public class PasswordChangeCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher)
    : ICommandHandler<PasswordChangeCommand>
{
    public async Task Handle(PasswordChangeCommand request, CancellationToken cancellationToken = default)
    {
        Domain.Entities.User.User? user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is not null)
        {
            string hash = passwordHasher.HashPassword(request.NewPassword);
            user.ChangePassword(request.NewPassword, hash);

            await unitOfWork.CompleteAsync(cancellationToken);
        }
    }
}