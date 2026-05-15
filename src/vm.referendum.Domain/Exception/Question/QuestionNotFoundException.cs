using Meadow_Framework.Core.Abstractions.Exceptions;

namespace vm.referendum.Domain.Exception.Question;

public class QuestionNotFoundException(string question)
    : InflowException("Question.NotFound", $"The Question : '{question}' Not Found");