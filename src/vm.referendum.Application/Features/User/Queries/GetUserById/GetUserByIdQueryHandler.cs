using Framework.Infrastructure.Exceptions;
using vm.referendum.Application.Contracts;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Application.Features.User.Queries.GetUserById;

/// <summary>
///     Represents the <see cref="GetUserByIdQuery" /> handler.
/// </summary>
internal sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    ///     Initializes a new instance of the <see cref="GetUserByIdQueryHandler" /> class.
    /// </summary>
    /// <param name="userIdentifierProvider">The user identifier provider.</param>
    /// <param name="dbContext">The database context.</param>
    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <inheritdoc />
    public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        Domain.Entities.User.User? user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
            throw new ObjectNotFoundException(typeof(UserResponse).ToString(), "request.UserId");
        UserResponse response = new()
        {
            Id = user.Id,
            Email = user.Email.Value,
            FullName = user.FirstName + " " + user.LastName,
            FirstName = user.FirstName,
            LastName = user.LastName
        };

        return response;
    }
}