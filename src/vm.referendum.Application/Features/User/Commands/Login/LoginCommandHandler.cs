using Framework.Abstractions.Exceptions;
using vm.referendum.Domain.Repository;
using vm.referendum.Domain.Services;
using vm.referendum.Domain.ValueObjects;
using vm.referendum.Infrastructure.Authentication.Authentication;

namespace vm.referendum.Application.Features.User.Commands.Login;

public sealed class LoginCommandHandler(
    IPasswordHashChecker passwordHashChecker,
    IJwtProvider jwtProvider,
    IUserRepository userRepository)
    : ICommandHandler<LoginCommand, string>
{
    public async Task<string> Handle(LoginCommand request,
        CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);
        // if (email.IsFailure) return Result.Failure<string>(email.Error);

        var user = await userRepository
            .GetByEmailAsync(email, cancellationToken);

        if (user is null)
            throw new InflowException("User not found");
            //return Result.Failure<string>(AuthenticationErrors.InvalidEmailOrPassword);


        var passwordValid = user.VerifyPasswordHash(request.Password, passwordHashChecker);

        if (!passwordValid) 
            throw new InflowException("Invalid password");
            //return Result.Failure<string>(AuthenticationErrors.InvalidEmailOrPassword);

        var token = await jwtProvider.GenerateAsync(user);

        return token;
    }
}