using vm.referendum.Domain.Abstractions;

namespace vm.referendum.Domain.Errors;

public class FirstNameErrors
{
    public static Error NullOrEmpty => new("FirstName.NullOrEmpty", "The first name is required.");

    public static Error LongerThanAllowed =>
        new("FirstName.LongerThanAllowed", "The first name is longer than allowed.");
}