using vm.referendum.Domain.Abstractions;
using vm.referendum.Domain.Errors;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Application.Features.Question.Commands.UpdateQuestion;

public class UpdateQuestionCommandHandler(IQuestionRepository questionRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateQuestionCommand, Result>
{
    public async Task<Result> Handle(UpdateQuestionCommand request,
        CancellationToken cancellationToken = default)
    {
        var question = await questionRepository.GetByIdAsync(request.QuestionId, cancellationToken);

        if (question is null)

            return Result.Failure(QuestionErrors.NotFound(request.QuestionId));

        var res = question.UpdateQuestionText(request.TextContent, request.UserId);
        if (res.IsFailure) return res;

        await unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}