using vm.referendum.Domain.Exception.Answer;
using vm.referendum.Domain.Exception.Question;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Application.Features.Answer.Commands.RemoveAnswer;

public class RemoveAnswerCommandHandler(IQuestionRepository questionRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveAnswerCommand>
{
    public async Task Handle(RemoveAnswerCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Question.Question? question = await questionRepository.GetByIdWithAnswersAsync(request.QuestionId, cancellationToken);

        if (question is null)
            throw new QuestionNotFoundException(request.QuestionId.ToString());

        Domain.Entities.Answer.Answer? answer = question.Answers.FirstOrDefault(a => a.Id == request.AnswerId);
        if (answer is null)
            throw new AnswerNotFoundException(request.AnswerId.ToString());

        question.RemoveAnswer(answer);

        await questionRepository.AddAsync(question, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
    }
}