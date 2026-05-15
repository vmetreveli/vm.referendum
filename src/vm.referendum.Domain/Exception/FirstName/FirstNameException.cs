using Meadow_Framework.Core.Abstractions.Exceptions;

namespace vm.referendum.Domain.Exception.FirstName;

public class FirstNameException(string code, string message)
    : InflowException(code, message);