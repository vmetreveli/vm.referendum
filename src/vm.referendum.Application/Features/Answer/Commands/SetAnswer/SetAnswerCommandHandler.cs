using vm.referendum.Domain.Abstractions;
using vm.referendum.Domain.Errors;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Application.Features.Answer.Commands.SetAnswer;

public sealed class SetAnswerCommandHandler : ICommandHandler<SetAnswerCommand, Result>
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SetAnswerCommandHandler(IQuestionRepository questionRepository, IUnitOfWork unitOfWork)
    {
        _questionRepository = questionRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result> Handle(SetAnswerCommand request, CancellationToken cancellationToken)
    {
        //TODO NotWorking Version
        var question = await _questionRepository.GetByIdWithAnswersAsync(request.QuestionId, cancellationToken);

        if (question is null) return Result.Failure(QuestionErrors.NotFound());

        var answer = question.Answers.FirstOrDefault(i => i.Id == request.AnswerId);

        if (answer is null) return Result.Failure(AnswerErrors.NotFound());

        var res = answer!.SetAnswer(question);
        if (res.IsFailure) return Result.Failure(res.Error);

        await _unitOfWork.CompleteAsync(cancellationToken);
        return Result.Success();
    }
}