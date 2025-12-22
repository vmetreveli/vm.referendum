using Asp.Versioning;
using vm.referendum.Api.Constants;
using vm.referendum.Api.Infrastructure;
using vm.referendum.Application.Contracts;
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
using vm.referendum.Domain.Entities.Permission;
using vm.referendum.Domain.Enums;
using vm.referendum.Infrastructure.Authentication;

namespace vm.referendum.Api.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.BaseRoute)]
public sealed class QuestionController(IDispatcher dispatcher) : ApiController(dispatcher)
{
    // [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
    [HasPermission(nameof(Permission.ReadMember))]
    [HttpGet]
    [Route(ApiRoutes.Questions.All)]
    [ApiSuccessResponse(StatusCodes.Status200OK)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<IActionResult> GetAllQuestions(CancellationToken cancellationToken)
    {
        //  var result = await Mediator.Send(new GetAllQuestionsQuery(), cancellationToken);
        // return result.IsSuccess ? Ok(result.Value) : NotFound();
        GetAllQuestionsQuery query = new();

        IReadOnlyList<QuestionResponse> res = await Dispatcher.QueryAsync(query, cancellationToken);

        return Ok(res);
    }

    [HttpGet]
    [Route(ApiRoutes.Questions.Id)]
    [ApiSuccessResponse(StatusCodes.Status200OK)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        Guid postId = id;
        GetQuestionByIdQuery query = new()
        {
            QuestionId = postId
        };
        QuestionResponse res = await Dispatcher.QueryAsync(query, cancellationToken);
        return Ok(res);
    }

    [HasPermission(nameof(Permission.UpdateMember))]
    [HttpPost]
    [ApiSuccessResponse(StatusCodes.Status204NoContent)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<IActionResult> CreateQuestion(
        [FromBody] CreateQuestionRequest createQuestionRequest,
        CancellationToken cancellationToken)
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        CreateQuestionCommand command = new()
        {
            UserId = userId,
            TextContent = createQuestionRequest.TextContent
        };
        await Dispatcher.SendAsync(command, cancellationToken);
        return NoContent();
    }

    [HasPermission(nameof(Permission.UpdateMember))]
    [HttpPut]
    [ApiSuccessResponse(StatusCodes.Status204NoContent)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<IActionResult> UpdateQuestion(
        [FromBody] UpdateQuestionRequest updateQuestionRequest,
        CancellationToken cancellationToken)
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        UpdateQuestionCommand command = new()
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
    [Route(ApiRoutes.Questions.Id)]
    [ApiSuccessResponse(StatusCodes.Status204NoContent)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<IActionResult> DeleteQuestion(
        Guid questionId,
        CancellationToken cancellationToken)
    {
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        DeleteQuestionCommand command = new()
        {
            QuestionId = questionId,
            UserId = userId
        };

        await Dispatcher.SendAsync(command, cancellationToken);

        return NoContent();
    }


    [HttpGet]
    [Route(ApiRoutes.Questions.Answers)]
    [ApiSuccessResponse(StatusCodes.Status200OK)]
    [ApiErrorResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<IActionResult> GetAnswersByQuestionId(
        Guid questionId,
        CancellationToken cancellationToken)
    {
        GetAnswersQuery query = new(questionId);
        IReadOnlyList<AnswerResponse> result = await Dispatcher.QueryAsync(query, cancellationToken);

        return Ok(result);
    }


    [HttpPost]
    [Route(ApiRoutes.Questions.Answers)]
    public async Task<IActionResult> AddAnswerToQuestion(
        Guid questionId,
        [FromBody] string answerText,
        CancellationToken cancellationToken)
    {
        //@TODO
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        AddAnswerCommand command = new(
            questionId,
            userId,
            answerText
        );

        AnswerResponse result = await Dispatcher.SendAsync(command, cancellationToken);

        return Ok(result);
    }


    [HttpDelete]
    [Route(ApiRoutes.Questions.AnswerById)]
    public async Task<IActionResult> RemoveAnswerFromQuestion(
        RemoveAnswerRequest request,
        CancellationToken cancellationToken)
    {
        RemoveAnswerCommand command = new(
            Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value),
            request.AnswerId,
            request.QuestionId
        );
        await Dispatcher.SendAsync(command, cancellationToken);
        return NoContent();
    }


    [HttpPut]
    [Route(ApiRoutes.Questions.AnswerById)]
    public async Task<IActionResult> UpdateAnswer(
        UpdateAnswerRequest request,
        CancellationToken cancellationToken)
    {
        UpdateAnswerCommand command = new(
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