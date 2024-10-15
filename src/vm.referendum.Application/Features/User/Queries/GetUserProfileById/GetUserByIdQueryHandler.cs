using vm.referendum.Application.Contracts;
using vm.referendum.Domain.Abstractions;
using vm.referendum.Domain.Errors;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Application.Features.User.Queries.GetUserProfileById;

public sealed class GetUserByIdQueryHandler
    : IQueryHandler<GetUserByIdQuery, Result<UserResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
            return Result.Failure<UserResponse>(UserErrors.NotFound(request.UserId));

        return _mapper.Map<UserResponse>(user);
    }
}