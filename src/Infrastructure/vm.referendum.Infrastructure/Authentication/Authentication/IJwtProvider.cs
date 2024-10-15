using vm.referendum.Domain.Entities;

namespace vm.referendum.Infrastructure.Authentication.Authentication;

/// <summary>
///     Represents the JWT provider interface.
/// </summary>
public interface IJwtProvider
{
    Task<string> GenerateAsync(User user);
}