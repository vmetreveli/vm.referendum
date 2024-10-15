using vm.referendum.Domain.Abstractions;
using vm.referendum.Domain.Errors;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Application.Features.Answer.Commands.UpdateAnswer;

public class UpdateAnswerCommandHandler : ICommandHandler<UpdateAnswerCommand, Result>
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IUnitOfWork _unitOfWork;


    public UpdateAnswerCommandHandler(IUnitOfWork unitOfWork, IQuestionRepository questionRepository)
    {
        _unitOfWork = unitOfWork;
        _questionRepository = questionRepository;
    }

    public async Task<Result> Handle(UpdateAnswerCommand request, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetByIdWithAnswersAsync(request.QuestionId, cancellationToken);

        if (question == null)
            // _result.AddError(ErrorCode.NotFound, QuestionsErrorMessage.QuestionNotFound);
            // return _result;
            return Result.Failure(QuestionErrors.NotFound());

        var answer = question.Answers.FirstOrDefault(a => a.Id == request.AnswerId);
        if (answer == null)
            return Result.Failure(AnswerErrors.NotFound());

        if (answer.UserId != request.UserId)
            // _result.AddError(ErrorCode.AnswerRemovalNotAuthorized,
            //     QuestionsErrorMessage.AnswerRemovalNotAuthorized);
            // return _result;
            return Result.Failure(UserErrors.InvalidPermissions);

        answer.UpdateAnswerText(request.Text);
        await _questionRepository.AddAsync(question, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}