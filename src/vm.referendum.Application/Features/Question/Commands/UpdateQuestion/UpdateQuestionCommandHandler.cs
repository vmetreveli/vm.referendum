using vm.referendum.Domain.Abstractions;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Application.Features.Question.Commands.UpdateQuestion;

public class UpdateQuestionCommandHandler(IQuestionRepository questionRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateQuestionCommand>
{
    public async Task Handle(UpdateQuestionCommand request,
        CancellationToken cancellationToken = default)
    {
        Domain.Entities.Question.Question? question = await questionRepository.GetByIdAsync(request.QuestionId, cancellationToken);

        if (question is null)
            throw new InflowException("Question not found");

        question.UpdateQuestionText(request.TextContent, request.UserId);

        await unitOfWork.CompleteAsync(cancellationToken);
    }
}