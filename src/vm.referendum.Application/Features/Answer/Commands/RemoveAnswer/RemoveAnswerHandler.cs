using vm.referendum.Domain.Abstractions;
using vm.referendum.Domain.Errors;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Application.Features.Answer.Commands.RemoveAnswer;

public class RemoveAnswerCommandHandler(IQuestionRepository questionRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveAnswerCommand, Result>
{
    public async Task<Result> Handle(RemoveAnswerCommand request, CancellationToken cancellationToken)
    {
        var question = await questionRepository.GetByIdWithAnswersAsync(request.QuestionId, cancellationToken);

        if (question == null)
            return Result.Failure(QuestionErrors.NotFound());

        var answer = question.Answers.FirstOrDefault(a => a.Id == request.AnswerId);
        if (answer == null)
            return Result.Failure(AnswerErrors.NotFound());

        var res = question.RemoveAnswer(answer);
        if (!res.IsFailure)
        {
            await questionRepository.AddAsync(question, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
        }

        return Result.Success();
    }
}