using AutoMapper.QueryableExtensions;
using Framework.Infrastructure.Exceptions;
using vm.referendum.Application.Contracts;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Application.Features.Question.Queries.GetAllQuestions;

public sealed class GetAllQuestionsQueryHandler(IQuestionRepository questionRepository, IMapper mapper) :
    IQueryHandler<GetAllQuestionsQuery, IReadOnlyList<QuestionResponse>>
{
    public async Task<IReadOnlyList<QuestionResponse>> Handle(GetAllQuestionsQuery request,
        CancellationToken cancellationToken)
    {
        var questions = await questionRepository.GetAllAsync(cancellationToken);
        var enumerable = questions.ToList();
        if (!enumerable.Any())
            throw new ObjectNotFoundException(typeof(QuestionResponse).ToString(),string.Empty);

        var res = enumerable.AsQueryable().ProjectTo<QuestionResponse>(mapper.ConfigurationProvider);
        return res.ToList();
    }
}