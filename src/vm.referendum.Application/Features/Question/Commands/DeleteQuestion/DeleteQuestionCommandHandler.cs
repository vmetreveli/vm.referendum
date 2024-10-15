using vm.referendum.Domain.Abstractions;
using vm.referendum.Domain.Errors;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Application.Features.Question.Commands.DeleteQuestion;

public class DeleteQuestionCommandHandler(IQuestionRepository questionRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteQuestionCommand, Result>
{
    public async Task<Result> Handle(DeleteQuestionCommand request,
        CancellationToken cancellationToken = default)
    {
        //  var result = new OperationResult<Domain.Core.Entities.Question>();

        var question = await questionRepository.GetByIdAsync(request.QuestionId, cancellationToken);

        if (question is null)
            return Result.Failure(QuestionErrors.NotFound(request.QuestionId));

        if (question.UserId != request.UserId)
            // result.AddError(ErrorCode.QuestionDeleteNotPossible, QuestionsErrorMessage.QuestionDeleteNotPossible);
            return Result.Failure(UserErrors.InvalidPermissions);

        questionRepository.Remove(question);
        await unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}