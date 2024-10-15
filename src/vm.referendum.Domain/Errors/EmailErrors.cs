using vm.referendum.Domain.Abstractions;

namespace vm.referendum.Domain.Errors;

public static class EmailErrors
{
    public static Error NullOrEmpty => new("Email.NullOrEmpty", "The email is required.");

    public static Error LongerThanAllowed => new("Email.LongerThanAllowed", "The email is longer than allowed.");

    public static Error InvalidFormat => new("Email.InvalidFormat", "The email format is invalid.");
}