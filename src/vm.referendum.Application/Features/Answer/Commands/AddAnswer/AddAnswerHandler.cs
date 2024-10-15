using vm.referendum.Application.Contracts;
using vm.referendum.Domain.Exception.Question;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Application.Features.Answer.Commands.AddAnswer;

public sealed class AddAnswerHandler(IUnitOfWork unitOfWork, IMapper mapper, IQuestionRepository questionRepository)
    : ICommandHandler<AddAnswerCommand, AnswerResponse>
{
    public async Task<AnswerResponse> Handle(AddAnswerCommand request, CancellationToken cancellationToken = default)
    {
        var question =
            await questionRepository
                .GetByIdAsync(request.QuestionId, cancellationToken);

        if (question is null)
            throw new QuestionNotFoundExceoption(request.QuestionId.ToString());

        var answer = Domain.Entities.Answer.CreateAnswer(request.QuestionId, request.Text, request.UserProfileId);

        question.AddAnswer(answer);

        await questionRepository.AddAsync(question, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return mapper.Map<AnswerResponse>(answer);
    }
}