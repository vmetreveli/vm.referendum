using vm.referendum.Domain.Exception.Answer;
using vm.referendum.Domain.Exception.Question;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Application.Features.Answer.Commands.UpdateAnswer;

public class UpdateAnswerCommandHandler(
    IUnitOfWork unitOfWork,
    IQuestionRepository questionRepository
) : ICommandHandler<UpdateAnswerCommand>
{
    public async Task Handle(UpdateAnswerCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Question.Question? question = await questionRepository.GetByIdWithAnswersAsync(request.QuestionId, cancellationToken);

        if (question is null)
            throw new QuestionNotFoundException(request.QuestionId.ToString());

        Domain.Entities.Answer.Answer? answer = question.Answers.FirstOrDefault(a => a.Id == request.AnswerId);
        if (answer is null)
            throw new AnswerNotFoundException(request.AnswerId.ToString());

        if (answer.UserId != request.UserId)
            throw new InflowException(AnswerErrors.InvalidPermissions.Code, AnswerErrors.InvalidPermissions.Name);

        answer.UpdateAnswerText(request.Text);
        await questionRepository.AddAsync(question, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
    }
}