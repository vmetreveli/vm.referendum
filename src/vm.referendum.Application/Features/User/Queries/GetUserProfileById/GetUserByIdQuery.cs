using vm.referendum.Application.Contracts;
using vm.referendum.Domain.Abstractions;

namespace vm.referendum.Application.Features.User.Queries.GetUserProfileById;

public sealed record GetUserByIdQuery(
    Guid UserId
) : IQuery<UserResponse>;