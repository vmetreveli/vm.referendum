using vm.referendum.Domain.Abstractions;

namespace vm.referendum.Domain.Errors;

public static class AuthenticationErrors
{
    public static Error InvalidEmailOrPassword => new(
        "Authentication.InvalidEmailOrPassword",
        "The specified email or password are incorrect.");
}