using Framework.Abstractions.Repository;
using vm.referendum.Domain.Entities;
using vm.referendum.Domain.Entities.User;
using vm.referendum.Domain.ValueObjects;

namespace vm.referendum.Domain.Repository;

public interface IUserRepository : IRepositoryBase<User, Guid>
{
    /// <summary>
    ///     Gets the user with the specified email.
    /// </summary>
    /// <param name="email">The user email.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The maybe instance that may contain the user with the specified email.</returns>
    Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken);

    /// <summary>
    ///     Checks if the specified email is unique.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>True if the specified email is unique, otherwise false.</returns>
    Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken);
}