using vm.referendum.Domain.Abstractions;

namespace vm.referendum.Application.Features.Answer.Commands.RemoveAnswer;

public sealed record RemoveAnswerCommand(
    Guid UserId,
    Guid QuestionId,
    Guid AnswerId
) : ICommand;