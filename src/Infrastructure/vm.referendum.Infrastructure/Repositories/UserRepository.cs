using Framework.Infrastructure.Repository;
using vm.referendum.Domain.Entities;
using vm.referendum.Domain.Repository;
using vm.referendum.Domain.ValueObjects;
using vm.referendum.Infrastructure.Specifications;

namespace vm.referendum.Infrastructure.Repositories;

/// <summary>
///     Represents the user repository.
/// </summary>
internal sealed class UserRepository(DbContext dbContext)
    : RepositoryBase<DbContext, User, Guid>(dbContext), IUserRepository
{
    public async Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken)
    {
        return !await ApplySpecification(new UserWithEmailSpecification(email))
            .AnyAsync(cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken)
    {
        return await ApplySpecification(new UserWithEmailSpecification(email))
            .FirstOrDefaultAsync(cancellationToken);
    }
}