using vm.referendum.Application.Contracts;

namespace vm.referendum.Application.Features.Question.Queries.GetAllQuestions;

public sealed class GetAllQuestionsQuery : IQuery<IReadOnlyList<QuestionResponse>>
{
}