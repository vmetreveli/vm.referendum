using vm.referendum.Application.Contracts;
using vm.referendum.Domain.Abstractions;

namespace vm.referendum.Application.Features.Question.Commands.CreateQuestion;

public class CreateQuestionCommand : ICommand<Result<QuestionResponse>>
{
    public string UserId { get; set; }
    public string TextContent { get; set; }
    public Guid CategoryId { get; set; }
}