using vm.referendum.Application.Contracts;
using vm.referendum.Domain.Abstractions;

namespace vm.referendum.Application.Features.Question.Queries.GetQuestionById;

public sealed class GetQuestionByIdQuery : IQuery<QuestionResponse>
{
    public Guid QuestionId { get; set; }
}