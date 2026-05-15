using Meadow_Framework.Core.Abstractions.Exceptions;

namespace vm.referendum.Domain.Exception.Answer;

public class AnswerNotFoundException(string answer)
    : InflowException("Answer.NotFound", $"The Answer : '{answer}' Not Found");