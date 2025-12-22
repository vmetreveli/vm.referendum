using AutoMapper.QueryableExtensions;
using vm.referendum.Application.Contracts;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Application.Features.Question.Queries.GetAllQuestions;

public sealed class GetAllQuestionsQueryHandler(IQuestionRepository questionRepository, IMapper mapper) :
    IQueryHandler<GetAllQuestionsQuery, IReadOnlyList<QuestionResponse>>
{
    public async Task<IReadOnlyList<QuestionResponse>> Handle(GetAllQuestionsQuery request,
        CancellationToken cancellationToken)
    {
        IEnumerable<Domain.Entities.Question.Question> questions = await questionRepository.GetAllAsync(cancellationToken);
        List<Domain.Entities.Question.Question> enumerable = questions.ToList();
        if (!enumerable.Any())
            throw new ObjectNotFoundException(typeof(QuestionResponse).ToString(), string.Empty);

        IQueryable<QuestionResponse>? res = enumerable.AsQueryable().ProjectTo<QuestionResponse>(mapper.ConfigurationProvider);
        return res.ToList();
    }
}