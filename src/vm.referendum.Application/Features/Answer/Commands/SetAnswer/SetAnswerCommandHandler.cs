using Framework.Abstractions.Exceptions;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Application.Features.Answer.Commands.SetAnswer;

public sealed class SetAnswerCommandHandler : ICommandHandler<SetAnswerCommand>
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SetAnswerCommandHandler(IQuestionRepository questionRepository, IUnitOfWork unitOfWork)
    {
        _questionRepository = questionRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task Handle(SetAnswerCommand request, CancellationToken cancellationToken)
    {
        //TODO NotWorking Version
        var question = await _questionRepository.GetByIdWithAnswersAsync(request.QuestionId, cancellationToken);

        if (question is null) throw new InflowException("Question not found");

        var answer = question.Answers.FirstOrDefault(i => i.Id == request.AnswerId);

        if (answer is null) throw new InflowException("Answer not found");

        answer!.SetAnswer(question);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }
}