using vm.referendum.Application.Contracts;

namespace vm.referendum.Application.Features.Answer.Commands.AddAnswer;

public sealed record AddAnswerCommand(
    Guid QuestionId,
    Guid UserProfileId,
    string Text
) : ICommand<AnswerResponse>;