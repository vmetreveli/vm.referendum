namespace vm.referendum.Domain.Exception.User;

public sealed class DuplicateEmailException()
    : InflowException("User.DuplicateEmail", "The specified email is already in use.");