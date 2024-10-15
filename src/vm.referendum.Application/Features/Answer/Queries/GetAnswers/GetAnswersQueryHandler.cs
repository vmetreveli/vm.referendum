using vm.referendum.Application.Contracts;
using vm.referendum.Domain.Abstractions;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Application.Features.Answer.Queries.GetAnswers;

public sealed class GetAnswersQueryHandler(IAnswerRepository answerRepository, IMapper mapper)
    : IQueryHandler<GetAnswersQuery, Result<IReadOnlyList<AnswerResponse>>>
{
    public async Task<Result<IReadOnlyList<AnswerResponse>>> Handle(GetAnswersQuery request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        // var res = await answerRepository.FindAsync(i => i.QuestionId == request.QuestionId, cancellationToken);
        //
        // if (!res.Any()) return Result.Failure<IReadOnlyList<AnswerResponse>>(AnswerErrors.NotFound());
        //
        // var mappedResult = res.AnswerResponse>(mapper.ConfigurationProvider)
        //     .AsEnumerable();
        // return mappedResult.ToList();
    }
}