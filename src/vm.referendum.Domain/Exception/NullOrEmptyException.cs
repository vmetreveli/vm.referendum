namespace vm.referendum.Domain.Exception;

public sealed class NullOrEmptyException(string message) : InflowException("NullOrEmpty", message);