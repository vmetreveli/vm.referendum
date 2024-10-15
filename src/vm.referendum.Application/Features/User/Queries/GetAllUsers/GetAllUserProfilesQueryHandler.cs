using vm.referendum.Application.Contracts;
using vm.referendum.Domain.Abstractions;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Application.Features.User.Queries.GetAllUsers;

public sealed class GetAllUsersQueryHandler(IMapper mapper, IUserRepository userRepository)
    : IQueryHandler<GetAllUsersQuery,
        IReadOnlyList<UserResponse>>
{
    public async Task<IReadOnlyList<UserResponse>> Handle(GetAllUsersQuery request,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
        // var users = await userRepository.GetAllAsync(cancellationToken);
        //
        // if (!users.Any()) return Result.Failure<IReadOnlyList<UserResponse>>(UserErrors.NotFound());
        // var res = users.ProjectTo<UserResponse>(mapper.ConfigurationProvider);
        //
        // return res.ToList();
    }
}