using Framework.Abstractions.Exceptions;

namespace vm.referendum.Domain.Exception.FirstNameErrors;

public class FirstNameException(string code, string message)
    : InflowException(code, message);