namespace vm.referendum.Domain.Exception.Email;

// public class NullOrEmptyException() : InflowException("FirstName.NullOrEmpty", "The first name is required.");
public class EmailException(string code, string message)
    : InflowException(code, message);