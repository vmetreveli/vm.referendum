using Framework.Abstractions.Exceptions;

namespace vm.referendum.Domain.Exception.FirstNameErrors;

// public class NullOrEmptyException() : InflowException("FirstName.NullOrEmpty", "The first name is required.");
public class LongerThanAllowedException()
    : InflowException("FirstName.LongerThanAllowed", "The first name is longer than allowed.");