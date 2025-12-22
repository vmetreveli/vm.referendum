using vm.referendum.Domain.Exception.User;
using vm.referendum.Domain.Repository;
using vm.referendum.Domain.ValueObjects;
using vm.referendum.Infrastructure.Authentication.Cryptography;

namespace vm.referendum.Application.Features.User.Commands.CreateUser;

/// <summary>
///     Represents the <see cref="CreateUserCommand" /> handler.
/// </summary>
internal sealed class CreateUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher)
    : ICommandHandler<CreateUserCommand, Guid>
{
    /// <inheritdoc />
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken = default)
    {
        string passwordResult = request.Password;
        FirstName firstNameResult = FirstName.Create(request.FirstName);
        LastName lastNameResult = LastName.Create(request.LastName);
        Email emailResult = Email.Create(request.Email);


        if (!await userRepository.IsEmailUniqueAsync(emailResult, cancellationToken))
            throw new DuplicateEmailException();

        string passwordHash = passwordHasher.HashPassword(passwordResult);

        Domain.Entities.User.User user = Domain.Entities.User.User.Create(
            firstNameResult,
            lastNameResult,
            emailResult,
            passwordHash);

        await userRepository.AddAsync(user, cancellationToken);

        await unitOfWork.CompleteAsync(cancellationToken);


        return user.Id;
    }
}