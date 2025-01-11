namespace vm.referendum.Application.Features.User.Commands.UpdateUser;

// /// <summary>
// ///     Represents the <see cref="UpdateUserCommand" /> handler.
// /// </summary>
// internal sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, Success>
// {
//     private readonly IUnitOfWork _unitOfWork;
//     private readonly IUserIdentifierProvider _userIdentifierProvider;
//     private readonly IUserRepository _userRepository;
//
//     /// <summary>
//     ///     Initializes a new instance of the <see cref="UpdateUserCommandHandler" /> class.
//     /// </summary>
//     /// <param name="userIdentifierProvider">The user identifier provider.</param>
//     /// <param name="userRepository">The user repository.</param>
//     /// <param name="unitOfWork">The unit of work.</param>
//     public UpdateUserCommandHandler(
//         IUserIdentifierProvider userIdentifierProvider,
//         IUserRepository userRepository,
//         IUnitOfWork unitOfWork)
//     {
//         _userIdentifierProvider = userIdentifierProvider;
//         _userRepository = userRepository;
//         _unitOfWork = unitOfWork;
//     }
//
//     /// <inheritdoc />
//     public async Task<ErrorOr<Success>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
//     {
//         if (request.UserId != _userIdentifierProvider.UserId)
//             return Error.Failure("DomainErrors.User.InvalidPermissions");
//
//         var firstNameResult = FirstName.Create(request.FirstName);
//         var lastNameResult = LastName.Create(request.LastName);
//
//
//         if (firstNameResult.IsError || lastNameResult.IsError) return Error.Failure("firstFailureOrSuccess.Error");
//
//         var maybeUser = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
//
//         if (maybeUser is null) return Error.Failure("DomainErrors.User.NotFound");
//
//         var user = maybeUser;
//
//         user.ChangeName(firstNameResult.Value, lastNameResult.Value);
//
//         await _unitOfWork.SaveChangesAsync(cancellationToken);
//
//         return Result.Success;
//     }
// }