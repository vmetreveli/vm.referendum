using vm.referendum.Application.Contracts;
using vm.referendum.Domain.Abstractions;
using vm.referendum.Domain.Repository;
using static System.Guid;

namespace vm.referendum.Application.Features.Question.Commands.CreateQuestion;

internal class CreateQuestionCommandHandler(
    IUserRepository userRepository,
    IQuestionRepository questionRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : ICommandHandler<CreateQuestionCommand, QuestionResponse>
{
    public async Task<QuestionResponse> Handle(
        CreateQuestionCommand request,
        CancellationToken cancellationToken = default)
    {
        if (!TryParse(request.UserId, out Guid userId)) throw new InflowException("Invalid user id.");


        Domain.Entities.User.User? user = await userRepository
            .GetByIdAsync(userId, cancellationToken);
        if (user is null) throw new InflowException("Invalid user id.");


        IEnumerable<Domain.Entities.Question.Question> checkQuestion =
            await questionRepository
                .FindAsync(i => i.TextContent == request.TextContent, cancellationToken);
        if (checkQuestion.Any()) throw new InflowException("Question already exists.");


        Domain.Entities.Question.Question question = Domain.Entities.Question.Question.CreateQuestion(
            userId,
            request.CategoryId,
            request.TextContent);

        await questionRepository.AddAsync(question, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);


        return mapper.Map<QuestionResponse>(question);
    }
}