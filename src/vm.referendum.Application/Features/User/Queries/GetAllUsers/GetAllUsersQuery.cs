using vm.referendum.Application.Contracts;
using vm.referendum.Domain.Abstractions;

namespace vm.referendum.Application.Features.User.Queries.GetAllUsers;

public sealed record GetAllUsersQuery : IQuery<IReadOnlyList<UserResponse>>;