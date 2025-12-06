using vm.referendum.Application.Contracts;
using vm.referendum.Domain.Exception.Question;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Application.Features.Answer.Commands.AddAnswer;

public sealed class AddAnswerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IQuestionRepository questionRepository)
    : ICommandHandler<AddAnswerCommand, AnswerResponse>
{
    public async Task<AnswerResponse> Handle(AddAnswerCommand request, CancellationToken cancellationToken = default)
    {
        Domain.Entities.Question.Question? question =
            await questionRepository
                .GetByIdAsync(request.QuestionId, cancellationToken);

        if (question is null)
            throw new QuestionNotFoundException(request.QuestionId.ToString());

        Domain.Entities.Answer.Answer answer = Domain.Entities.Answer.Answer.CreateAnswer(request.QuestionId, request.Text, request.UserProfileId);

        question.AddAnswer(answer);

        await questionRepository.AddAsync(question, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return mapper.Map<AnswerResponse>(answer);
    }
}