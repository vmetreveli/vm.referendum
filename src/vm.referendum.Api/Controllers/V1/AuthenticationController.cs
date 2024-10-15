using Asp.Versioning;
using Framework.Abstractions.Dispatchers;
using Microsoft.AspNetCore.RateLimiting;
using vm.referendum.Api.Constants;
using vm.referendum.Api.Infrastructure;
using vm.referendum.Application.Features.User.Commands.CreateUser;
using vm.referendum.Application.Features.User.Commands.Login;
using vm.referendum.Application.Features.User.Commands.PasswordReset;
using vm.referendum.Domain.Enums;
using vm.referendum.Infrastructure.Authentication;

namespace vm.referendum.Api.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.BASE_ROUTE)]
public sealed class AuthenticationController(IDispatcher dispatcher) : ApiController(dispatcher)
{
    [AllowAnonymous]
    [HttpPost(ApiRoutes.Authentication.LOGIN)]
    [ApiSuccessResponse(StatusCodes.Status200OK)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    [EnableRateLimiting(PolicyNames.AuthenticatedUserPolicy)]
    public async Task<IActionResult> Login(
        LoginCommand loginRequest,
        CancellationToken cancellationToken)
    {
        var res=await Dispatcher.SendAsync(loginRequest, cancellationToken);
       return Ok(res);
    }


    [AllowAnonymous]
    [HttpPost(ApiRoutes.Authentication.REGISTER)]
    [ApiSuccessResponse(StatusCodes.Status200OK)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<IActionResult> Create(
        CreateUserCommand createUserCommand,
        CancellationToken cancellationToken)
    {
        var res = await Dispatcher.SendAsync(createUserCommand, cancellationToken);

        return Ok(res);
    }


    [HasPermission(Permission.UpdateMember)]
    [HttpDelete]
    [ApiSuccessResponse(StatusCodes.Status200OK)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<IActionResult> DeleteAccount(string identityUserId, CancellationToken cancellationToken)
    {
        // var identityUserGuid = Guid.Parse(identityUserId);
        // var requestorGuid = HttpContext.GetIdentityIdClaimValue();
        // var command = new RemoveAccount
        // {
        //     IdentityUserId = identityUserGuid,
        //     RequestorGuid = requestorGuid
        // };
        //
        // var result = await _mediator.Send(command, cancellationToken);
        //
        // if (result.IsError) return HandleErrorResponse(result.Errors);
        return NoContent();
    }

    [AllowAnonymous]
    [HttpPost("ForgotPassword")]
    [ApiSuccessResponse(StatusCodes.Status200OK)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<IActionResult> ForgotPassword(PasswordResetCommand command, CancellationToken cancellationToken)
    {
        var recovery =
            await Dispatcher.SendAsync(command, cancellationToken);

      return  Ok(recovery);
    }
}