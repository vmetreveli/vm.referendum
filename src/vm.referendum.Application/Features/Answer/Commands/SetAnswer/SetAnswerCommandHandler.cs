using vm.referendum.Domain.Exception.Answer;
using vm.referendum.Domain.Exception.Question;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Application.Features.Answer.Commands.SetAnswer;

public sealed class SetAnswerCommandHandler(IQuestionRepository questionRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<SetAnswerCommand>
{
    public async Task Handle(SetAnswerCommand request, CancellationToken cancellationToken)
    {
        //TODO NotWorking Version
        Domain.Entities.Question.Question? question = await questionRepository.GetByIdWithAnswersAsync(request.QuestionId, cancellationToken);

        if (question is null) throw new QuestionNotFoundException(request.QuestionId.ToString());

        Domain.Entities.Answer.Answer? answer = question.Answers.FirstOrDefault(i => i.Id == request.AnswerId);

        if (answer is null) throw new AnswerNotFoundException(request.AnswerId.ToString());

        answer!.SetAnswer(question);
        await unitOfWork.CompleteAsync(cancellationToken);
    }
}