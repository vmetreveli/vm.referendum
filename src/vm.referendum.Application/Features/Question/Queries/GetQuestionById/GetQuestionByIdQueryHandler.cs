using Framework.Infrastructure.Exceptions;
using vm.referendum.Application.Contracts;
using vm.referendum.Domain.Abstractions;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Application.Features.Question.Queries.GetQuestionById;

public sealed class GetQuestionByIdQueryHandler(
    IMapper mapper,
    IQuestionRepository questionRepository,
    IUnitOfWork unitOfWork)
    : IQueryHandler<GetQuestionByIdQuery, QuestionResponse>
{
    public async Task<QuestionResponse> Handle(GetQuestionByIdQuery request,
        CancellationToken cancellationToken = default)
    {
        Domain.Entities.Question.Question? question =
            await questionRepository.GetByIdWithAnswersAsync(request.QuestionId, cancellationToken);

        if (question is null)
            throw new ObjectNotFoundException(typeof(QuestionResponse).ToString(), request.QuestionId.ToString());

        QuestionResponse? mapped = mapper.Map<QuestionResponse>(question);

        return mapped;
    }
}