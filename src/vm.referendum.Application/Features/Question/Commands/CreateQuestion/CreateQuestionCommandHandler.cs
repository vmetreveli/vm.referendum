using vm.referendum.Application.Contracts;
using vm.referendum.Domain.Abstractions;
using vm.referendum.Domain.Errors;
using vm.referendum.Domain.Repository;
using static System.Guid;

namespace vm.referendum.Application.Features.Question.Commands.CreateQuestion;

internal class CreateQuestionCommandHandler(
    IUserRepository userRepository,
    IQuestionRepository questionRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : ICommandHandler<CreateQuestionCommand, Result<QuestionResponse>>
{
    public async Task<Result<QuestionResponse>> Handle(
        CreateQuestionCommand request,
        CancellationToken cancellationToken = default)
    {
        if (!TryParse(request.UserId, out var userId)) Result.Failure<QuestionResponse>(UserErrors.NotFound());


        var user = await userRepository
            .GetByIdAsync(userId, cancellationToken);
        if (user is null) return Result.Failure<QuestionResponse>(UserErrors.NotFound(userId));


        var checkQuestion =
            await questionRepository
                .FindAsync(i => i.TextContent == request.TextContent, cancellationToken);
        if (checkQuestion.Any()) return Result.Failure<QuestionResponse>(QuestionErrors.Validation());


        var question = Domain.Entities.Question.CreateQuestion(
            userId,
            request.CategoryId,
            request.TextContent);

        await questionRepository.AddAsync(question, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);


        return mapper.Map<QuestionResponse>(question);
    }
}