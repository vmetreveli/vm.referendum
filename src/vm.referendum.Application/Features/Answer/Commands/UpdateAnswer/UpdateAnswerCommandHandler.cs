using Framework.Abstractions.Exceptions;
using vm.referendum.Domain.Abstractions;
using vm.referendum.Domain.Errors;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Application.Features.Answer.Commands.UpdateAnswer;

public class UpdateAnswerCommandHandler : ICommandHandler<UpdateAnswerCommand>
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IUnitOfWork _unitOfWork;


    public UpdateAnswerCommandHandler(IUnitOfWork unitOfWork, IQuestionRepository questionRepository)
    {
        _unitOfWork = unitOfWork;
        _questionRepository = questionRepository;
    }

    public async Task Handle(UpdateAnswerCommand request, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetByIdWithAnswersAsync(request.QuestionId, cancellationToken);

        if (question == null)
            throw new InflowException("Question not found");

        var answer = question.Answers.FirstOrDefault(a => a.Id == request.AnswerId);
        if (answer == null)
            throw new InflowException("Answer not found");

        if (answer.UserId != request.UserId)
            // _result.AddError(ErrorCode.AnswerRemovalNotAuthorized,
            //     QuestionsErrorMessage.AnswerRemovalNotAuthorized);
            // return _result;
            throw new InflowException("You are not authorized to update this answer");

        answer.UpdateAnswerText(request.Text);
        await _questionRepository.AddAsync(question, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }
}