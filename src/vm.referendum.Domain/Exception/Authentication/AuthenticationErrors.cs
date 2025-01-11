using vm.referendum.Domain.Abstractions;

namespace vm.referendum.Domain.Exception.Authentication;

public static class AuthenticationErrors
{
    public static Error InvalidEmailOrPassword => new(
        "Authentication.InvalidEmailOrPassword",
        "The specified email or password are incorrect.");
}