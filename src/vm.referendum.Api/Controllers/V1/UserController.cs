using Asp.Versioning;
using Framework.Abstractions.Dispatchers;
using vm.referendum.Api.Infrastructure;
using vm.referendum.Application.Features.User.Commands.DeleteUser;
using vm.referendum.Application.Features.User.Commands.PasswordChange;
using vm.referendum.Application.Features.User.Commands.UpdateUser;
using vm.referendum.Application.Features.User.Queries.GetAllUsers;
using vm.referendum.Application.Features.User.Queries.GetUserById;
using vm.referendum.Domain.Enums;
using vm.referendum.Infrastructure.Authentication;

namespace vm.referendum.Api.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.BASE_ROUTE)]
public class UserController(IDispatcher dispatcher) : ApiController(dispatcher)
{
    [HasPermission(Permission.ReadMember)]
    [HttpGet]
    [ApiSuccessResponse(StatusCodes.Status200OK)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<IActionResult> GetAllProfiles(CancellationToken cancellationToken)
    {
        var query = new GetAllUsersQuery();
        var res = await Dispatcher.QueryAsync(query, cancellationToken);
        return Ok(res);
    }


    [HasPermission(Permission.ReadMember)]
    [Route(ApiRoutes.UserProfiles.ID_ROUTE)]
    [HttpGet]
    [ApiSuccessResponse(StatusCodes.Status200OK)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<IActionResult> GetUserProfileById(
        GetUserByIdQuery query,
        CancellationToken cancellationToken)
    {
        var res = await Dispatcher.QueryAsync(query, cancellationToken);
        return Ok(res);
    }

    [HasPermission(Permission.UpdateMember)]
    [HttpPut]
    [Route(ApiRoutes.UserProfiles.ID_ROUTE)]
    [ApiSuccessResponse(StatusCodes.Status204NoContent)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<IActionResult> UpdateUserProfile(
        Guid id,
        UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        await Dispatcher.SendAsync(command, cancellationToken);
        return NoContent();
    }

    [HasPermission(Permission.UpdateMember)]
    [HttpPut("ChangePassword")]
    [ApiSuccessResponse(StatusCodes.Status204NoContent)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<IActionResult> ChangePassword(
        PasswordChangeCommand command,
        CancellationToken cancellationToken)
    {
        await Dispatcher.SendAsync(command, cancellationToken);
        return NoContent();
    }


    [HasPermission(Permission.UpdateMember)]
    [HttpDelete("DeleteProfile")]
    [ApiSuccessResponse(StatusCodes.Status204NoContent)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<IActionResult> DeleteProfile(
        DeleteUserCommand command,
        CancellationToken cancellationToken)
    {
        await Dispatcher.SendAsync(command, cancellationToken);
        return NoContent();
    }
}