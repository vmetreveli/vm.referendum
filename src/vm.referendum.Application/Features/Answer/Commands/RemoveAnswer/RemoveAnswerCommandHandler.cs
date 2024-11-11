using Framework.Abstractions.Exceptions;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Application.Features.Answer.Commands.RemoveAnswer;

public class RemoveAnswerCommandHandler(IQuestionRepository questionRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveAnswerCommand>
{
    public async Task Handle(RemoveAnswerCommand request, CancellationToken cancellationToken)
    {
        var question = await questionRepository.GetByIdWithAnswersAsync(request.QuestionId, cancellationToken);

        if (question == null)
            throw new InflowException("Question not found");

        var answer = question.Answers.FirstOrDefault(a => a.Id == request.AnswerId);
        if (answer == null)
            throw new InflowException("Answer not found");

        question.RemoveAnswer(answer);

        await questionRepository.AddAsync(question, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
    }
}