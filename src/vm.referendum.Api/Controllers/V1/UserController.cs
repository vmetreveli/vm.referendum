using Asp.Versioning;
using Framework.Abstractions.Dispatchers;
using vm.referendum.Api.Infrastructure;
using vm.referendum.Application.Contracts;
using vm.referendum.Application.Features.User.Commands.DeleteUser;
using vm.referendum.Application.Features.User.Commands.PasswordChange;
using vm.referendum.Application.Features.User.Commands.UpdateUser;
using vm.referendum.Application.Features.User.Queries.GetAllUsers;
using vm.referendum.Application.Features.User.Queries.GetUserById;
using vm.referendum.Domain.Entities.Permission;
using vm.referendum.Domain.Enums;
using vm.referendum.Infrastructure.Authentication;

namespace vm.referendum.Api.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.BaseRoute)]
public class UserController(IDispatcher dispatcher) : ApiController(dispatcher)
{
    [HasPermission(nameof(Permission.ReadMember))]
    [HttpGet]
    [ApiSuccessResponse(StatusCodes.Status200OK)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<IActionResult> GetAllProfiles(CancellationToken cancellationToken)
    {
        GetAllUsersQuery query = new();
        IReadOnlyList<UserResponse> res = await Dispatcher.QueryAsync(query, cancellationToken);
        return Ok(res);
    }


    [HasPermission(nameof(Permission.ReadMember))]
    [Route(ApiRoutes.UserProfiles.IdRoute)]
    [HttpGet]
    [ApiSuccessResponse(StatusCodes.Status200OK)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<IActionResult> GetUserProfileById(
        GetUserByIdQuery query,
        CancellationToken cancellationToken)
    {
        UserResponse res = await Dispatcher.QueryAsync(query, cancellationToken);
        return Ok(res);
    }

    [HasPermission(nameof(Permission.UpdateMember))]
    [HttpPut]
    [Route(ApiRoutes.UserProfiles.IdRoute)]
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

    [HasPermission(nameof(Permission.UpdateMember))]
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


    [HasPermission(nameof(Permission.UpdateMember))]
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