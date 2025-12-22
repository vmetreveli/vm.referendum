using vm.referendum.Application.Contracts;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Application.Features.User.Queries.GetUserProfileById;

public sealed class GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
    : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    public async Task<UserResponse> Handle(GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        Domain.Entities.User.User? user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
            throw new ObjectNotFoundException(nameof(Domain.Entities.User.User), request.UserId.ToString());

        return mapper.Map<UserResponse>(user);
    }
}