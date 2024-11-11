using vm.referendum.Domain.Abstractions;

namespace vm.referendum.Application.Features.Question.Commands.DeleteQuestion;

public sealed class DeleteQuestionCommand : ICommand
{
    public Guid QuestionId { get; set; }
    public Guid UserId { get; set; }
}