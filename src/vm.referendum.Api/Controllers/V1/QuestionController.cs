using Asp.Versioning;
using Framework.Abstractions.Dispatchers;
using vm.referendum.Api.Constants;
using vm.referendum.Api.Infrastructure;
using vm.referendum.Application.Features.Answer.Commands.AddAnswer;
using vm.referendum.Application.Features.Answer.Commands.RemoveAnswer;
using vm.referendum.Application.Features.Answer.Commands.UpdateAnswer;
using vm.referendum.Application.Features.Answer.Queries.GetAnswers;
using vm.referendum.Application.Features.Question.Commands.CreateQuestion;
using vm.referendum.Application.Features.Question.Commands.DeleteQuestion;
using vm.referendum.Application.Features.Question.Commands.UpdateQuestion;
using vm.referendum.Application.Features.Question.Queries.GetAllQuestions;
using vm.referendum.Application.Features.Question.Queries.GetQuestionById;
using vm.referendum.Application.Features.SeedData.Commands.AddDumpData;
using vm.referendum.Domain.Enums;
using vm.referendum.Infrastructure.Authentication;

namespace vm.referendum.Api.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.BASE_ROUTE)]
public sealed class QuestionController(IDispatcher dispatcher) : ApiController(dispatcher)
{
    // [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
    // [HasPermission(Permission.ReadMember)]
    [HttpGet]
    [Route(ApiRoutes.Questions.All_Question)]
    [ApiSuccessResponse(StatusCodes.Status200OK)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<IActionResult> GetAllQuestions(CancellationToken cancellationToken)
    {
        //  var result = await Mediator.Send(new GetAllQuestionsQuery(), cancellationToken);
        // return result.IsSuccess ? Ok(result.Value) : NotFound();
        var query = new GetAllQuestionsQuery();

        var res = await Dispatcher.QueryAsync(query, cancellationToken);

        return Ok(res);
    }

    [HttpGet]
    [Route(ApiRoutes.Questions.ID_ROUTE)]
    [ApiSuccessResponse(StatusCodes.Status200OK)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var postId = id;
        var query = new GetQuestionByIdQuery { QuestionId = postId };
        var res = await Dispatcher.QueryAsync(query, cancellationToken);
        return Ok(res);
    }

    [HasPermission(Permission.UpdateMember)]
    [HttpPost]
    [ApiSuccessResponse(StatusCodes.Status204NoContent)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<IActionResult> CreateQuestion(
        [FromBody] CreateQuestionRequest createQuestionRequest,
        CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var command = new CreateQuestionCommand
        {
            UserId = userId,
            TextContent = createQuestionRequest.TextContent
        };
        await Dispatcher.SendAsync(command, cancellationToken);
        return NoContent();
    }

    [HasPermission(Permission.UpdateMember)]
    [HttpPut]
    [ApiSuccessResponse(StatusCodes.Status204NoContent)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<IActionResult> UpdateQuestion(
        [FromBody] UpdateQuestionRequest updateQuestionRequest,
        CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var command = new UpdateQuestionCommand
        {
            UserId = Guid.Parse(userId),
            TextContent = updateQuestionRequest.TextContent,
            // CategoryId = updateQuestion.CategoryId,
            QuestionId = updateQuestionRequest.QuestionId
        };
        await Dispatcher.SendAsync(command, cancellationToken);
        return NoContent();
    }


    [HttpDelete]
    [Route(ApiRoutes.Questions.ID_ROUTE)]
    [ApiSuccessResponse(StatusCodes.Status204NoContent)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<IActionResult> DeleteQuestion(
        Guid questionId,
        CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var command = new DeleteQuestionCommand { QuestionId = questionId, UserId = userId };

        await Dispatcher.SendAsync(command, cancellationToken);

        return NoContent();
    }


    [HttpGet]
    [Route(ApiRoutes.Questions.ANSWERS)]
    [ApiSuccessResponse(StatusCodes.Status200OK)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<IActionResult> GetAnswersByQuestionId(
        Guid questionId,
        CancellationToken cancellationToken)
    {
        var query = new GetAnswersQuery(questionId);
        var result = await Dispatcher.QueryAsync(query, cancellationToken);

        return Ok(result.Value);
    }


    [HttpPost]
    [Route(ApiRoutes.Questions.ANSWERS)]
    public async Task<IActionResult> AddAnswerToQuestion(
        Guid questionId,
        [FromBody] string answerText,
        CancellationToken cancellationToken)
    {
        //@TODO
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var command = new AddAnswerCommand
        (
            questionId,
            userId,
            answerText
        );

        var result = await Dispatcher.SendAsync(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }


    [HttpDelete]
    [Route(ApiRoutes.Questions.ANSWER_BY_ID)]
    public async Task<IActionResult> RemoveAnswerFromQuestion(
        RemoveAnswerRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RemoveAnswerCommand
        (
            Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value),
            request.AnswerId,
            request.QuestionId
        );
        await Dispatcher.SendAsync(command, cancellationToken);
        return NoContent();
    }


    [HttpPut]
    [Route(ApiRoutes.Questions.ANSWER_BY_ID)]
    public async Task<IActionResult> UpdateAnswer(
        UpdateAnswerRequest request,
        CancellationToken cancellationToken)
    {
        var command=new UpdateAnswerCommand
            (
                Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value),
                request.QuestionId,
                request.AnswerId,
                request.Text
            );
           await Dispatcher.SendAsync(command, cancellationToken);
          return NoContent();
    }

    // [HttpPost]
    // [Route("Seed")]
    // public async Task<IActionResult> SeedDb(CancellationToken cancellationToken)
    // {
    //     return await Result.Create(new AddDumpDataCommand())
    //         .Map(req => new AddDumpDataCommand())
    //         .Bind(command => Dispatcher.SendAsync(command, cancellationToken))
    //         .Match<IActionResult>(NoContent, BadRequest);
    // }
}