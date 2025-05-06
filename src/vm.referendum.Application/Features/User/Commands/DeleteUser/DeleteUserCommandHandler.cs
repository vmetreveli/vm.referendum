using vm.referendum.Domain.Abstractions;
using vm.referendum.Domain.Repository;
using vm.referendum.Domain.ValueObjects;

namespace vm.referendum.Application.Features.User.Commands.DeleteUser;

internal sealed class DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteUserCommand>
{
    /// <inheritdoc />
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        Email emailResult = Email.Create(request.Email);
        Domain.Entities.User.User? user = await userRepository.GetByEmailAsync(emailResult, cancellationToken);

        if (user is not null)
        {
            userRepository.Remove(user);
            await unitOfWork.CompleteAsync(cancellationToken);
        }
    }
}