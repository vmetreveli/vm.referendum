using Framework.Abstractions.Dispatchers;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace vm.referendum.Api.Infrastructure;

[Authorize]
[ApiController]
[ApiErrorResponse(500, "Internal Server Error")]
public class ApiController : ControllerBase
{
    protected IDispatcher Dispatcher { get; }

    protected ApiController(IDispatcher dispatcher)
    {
        Dispatcher = dispatcher;
    }

    [NonAction]
    protected ObjectResult CreatedResult([ActionResultObjectValue] object value)
    {
        return new ObjectResult(value)
        {
            StatusCode = StatusCodes.Status201Created
        };
    }
}