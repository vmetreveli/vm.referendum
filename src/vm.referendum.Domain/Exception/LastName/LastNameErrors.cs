using vm.referendum.Domain.Abstractions;

namespace vm.referendum.Domain.Exception.LastName;

public static class LastNameErrors
{
    public static Error NullOrEmpty => new("LastName.NullOrEmpty", "The last name is required.");

    public static Error LongerThanAllowed => new("LastName.LongerThanAllowed", "The last name is longer than allowed.");
}