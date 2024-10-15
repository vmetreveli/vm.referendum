namespace vm.referendum.Domain.Abstractions;

public record Error
{
    public static readonly Error None = new();

    public static readonly Error NullValue = new("Error.NullValue", "Null value was provided");

    public Error()
    {
    }

    public Error(string Code, string Name)
    {
    }

    public string Code { get; init; }
    public string Name { get; init; }
}