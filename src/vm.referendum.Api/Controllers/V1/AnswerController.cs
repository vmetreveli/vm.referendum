using Asp.Versioning;
using Framework.Abstractions.Dispatchers;
using vm.referendum.Api.Infrastructure;
using vm.referendum.Application.Features.Answer.Commands.SetAnswer;

namespace vm.referendum.Api.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.BaseRoute)]
public sealed class AnswerController(IDispatcher dispatcher) : ApiController(dispatcher)
{
    [Route(ApiRoutes.Answers.Set)]
    [HttpPut]
    [ApiSuccessResponse(StatusCodes.Status204NoContent)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<IActionResult> SetAnswer(
        [FromBody] SetAnswerCommand command,
        CancellationToken cancellationToken)
    {
        await Dispatcher.SendAsync(command, cancellationToken);
        return NoContent();
    }
}