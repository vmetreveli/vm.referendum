using vm.referendum.Application.Contracts;
using vm.referendum.Domain.Abstractions;

namespace vm.referendum.Application.Features.User.Queries.GetUserById;

/// <summary>
///     Represents the query for getting a user by identifier.
/// </summary>
public sealed record GetUserByIdQuery(
    Guid UserId
) : IQuery<UserResponse>;