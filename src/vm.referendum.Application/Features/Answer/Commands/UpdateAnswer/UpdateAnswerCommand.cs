using vm.referendum.Domain.Abstractions;

namespace vm.referendum.Application.Features.Answer.Commands.UpdateAnswer;

public sealed record UpdateAnswerCommand(
    Guid UserId,
    Guid QuestionId,
    Guid AnswerId,
    string Text
) : ICommand<Result>;