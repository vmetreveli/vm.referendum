using Framework.Abstractions.Exceptions;
using vm.referendum.Domain.Abstractions;
using vm.referendum.Domain.Errors;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Application.Features.Question.Commands.DeleteQuestion;

public class DeleteQuestionCommandHandler(IQuestionRepository questionRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteQuestionCommand>
{
    public async Task Handle(DeleteQuestionCommand request,
        CancellationToken cancellationToken = default)
    {
        //  var result = new OperationResult<Domain.Core.Entities.Question>();

        var question = await questionRepository.GetByIdAsync(request.QuestionId, cancellationToken);

        if (question is null)
           throw new InflowException("Question not found");

        if (question.UserId != request.UserId)
           throw new UnauthorizedAccessException("You are not authorized to delete this question");

        questionRepository.Remove(question);
        await unitOfWork.CompleteAsync(cancellationToken);
 }
}