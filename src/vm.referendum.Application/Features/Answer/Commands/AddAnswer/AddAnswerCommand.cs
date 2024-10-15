using vm.referendum.Application.Contracts;
using vm.referendum.Domain.Abstractions;

namespace vm.referendum.Application.Features.Answer.Commands.AddAnswer;

public sealed record AddAnswerCommand(
    Guid QuestionId,
    Guid UserProfileId,
    string Text
) : ICommand<Result<AnswerResponse>>;