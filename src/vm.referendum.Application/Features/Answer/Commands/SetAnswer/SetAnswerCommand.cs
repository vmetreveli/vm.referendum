using vm.referendum.Domain.Abstractions;

namespace vm.referendum.Application.Features.Answer.Commands.SetAnswer;

public sealed record SetAnswerCommand(
    Guid AnswerId,
    Guid QuestionId
) : ICommand;