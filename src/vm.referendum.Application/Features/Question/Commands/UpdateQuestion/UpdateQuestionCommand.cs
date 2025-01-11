using vm.referendum.Domain.Abstractions;

namespace vm.referendum.Application.Features.Question.Commands.UpdateQuestion;

public class UpdateQuestionCommand : ICommand
{
    public string TextContent { get; set; }
    public Guid CategoryId { get; set; }
    public Guid UserId { get; set; }
    public Guid QuestionId { get; set; }
}