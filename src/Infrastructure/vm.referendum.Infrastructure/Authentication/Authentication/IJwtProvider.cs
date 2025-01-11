using vm.referendum.Domain.Entities;
using vm.referendum.Domain.Entities.User;

namespace vm.referendum.Infrastructure.Authentication.Authentication;

/// <summary>
///     Represents the JWT provider interface.
/// </summary>
public interface IJwtProvider
{
    Task<string> GenerateAsync(User user);
}