using Framework.Abstractions.Dispatchers;

namespace vm.referendum.Api.Infrastructure;

[ApiController]
//[Authorize]
public class ApiController : ControllerBase
{
    protected ApiController(IDispatcher dispatcher)
    {
        Dispatcher = dispatcher;
    }

    protected IDispatcher Dispatcher { get; }
}