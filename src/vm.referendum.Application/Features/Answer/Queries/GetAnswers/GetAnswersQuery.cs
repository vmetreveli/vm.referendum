using vm.referendum.Application.Contracts;
using vm.referendum.Domain.Abstractions;

namespace vm.referendum.Application.Features.Answer.Queries.GetAnswers;

public sealed record GetAnswersQuery(
    Guid QuestionId
) : IQuery<Result<IReadOnlyList<AnswerResponse>>>;