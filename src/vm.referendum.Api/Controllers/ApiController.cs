using Microsoft.AspNetCore.Mvc;
using Framework.Abstractions.Dispatchers;
using vm.referendum.Api.Routes;

namespace  vm.referendum.Api.Controllers;

[ApiController]
[Route(ApiRoutes.BaseRoute)]
public class ApiController : ControllerBase
{
    protected ApiController(IDispatcher dispatcher)
    {
        Dispatcher = dispatcher;
    }

    protected IDispatcher Dispatcher { get; }
}