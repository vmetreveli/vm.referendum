using Asp.Versioning;
using Framework.Abstractions.Dispatchers;
using vm.referendum.Api.Infrastructure;
using vm.referendum.Application.Features.Category.Commands.CreateCategory;
using vm.referendum.Application.Features.Category.Commands.DeleteCategory;
using vm.referendum.Application.Features.Category.Commands.UpdateCategory;
using vm.referendum.Application.Features.Category.Queries.GetAllCategory;
using vm.referendum.Domain.Enums;
using vm.referendum.Infrastructure.Authentication;

namespace vm.referendum.Api.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.BASE_ROUTE)]
public class CategoryController : ApiController
{
    protected CategoryController(IDispatcher dispatcher) : base(dispatcher)
    {
    }

    [HasPermission(Permission.UpdateMember)]
    [HttpPost]
    [ApiSuccessResponse(StatusCodes.Status204NoContent)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<IActionResult> CreateCategory(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        await Dispatcher.SendAsync(command, cancellationToken);

        return NoContent();
    }

    [HasPermission(Permission.UpdateMember)]
    [HttpPut]
    [ApiSuccessResponse(StatusCodes.Status204NoContent)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        await Dispatcher.SendAsync(command, cancellationToken);
        return NoContent();
    }

    [HasPermission(Permission.UpdateMember)]
    [HttpDelete]
    [ApiSuccessResponse(StatusCodes.Status204NoContent)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<IActionResult> DeleteCategory(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        await Dispatcher.SendAsync(command, cancellationToken);
        return NoContent();
    }

    [HasPermission(Permission.ReadMember)]
    [HttpGet]
    [ApiSuccessResponse(StatusCodes.Status200OK)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<IActionResult> GetAllCategory(GetAllCategoryQuery query, CancellationToken cancellationToken)
    {
        var result = await Dispatcher.QueryAsync(query, cancellationToken);

        return Ok(result);
    }
}