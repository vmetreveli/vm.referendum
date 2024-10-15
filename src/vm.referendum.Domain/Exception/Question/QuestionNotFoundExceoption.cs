using Framework.Abstractions.Exceptions;

namespace vm.referendum.Domain.Exception.Question;

public class QuestionNotFoundExceoption(string question) : InflowException($"The Question : '{question}' Not Found");