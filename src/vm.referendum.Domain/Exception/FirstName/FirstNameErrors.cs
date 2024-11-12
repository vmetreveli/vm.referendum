using vm.referendum.Domain.Abstractions;

namespace vm.referendum.Domain.Exception.FirstNameErrors;

public static class FirstNameErrors
{
    public static Error NullOrEmpty => new("Name.NullOrEmpty", "The name is required.");

    public static Error LongerThanAllowed => new("Name.LongerThanAllowed", "The name is longer than allowed.");
}