using vm.referendum.Domain.Exception;

namespace vm.referendum.Domain.ValueObjects;

public sealed class LastName : ValueObject
{
    private LastName()
    {
    }

    private LastName(string value)
    {
        Value = value;
    }

    public string? Value { get; set; }

    public static LastName Create(string? lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            throw new NullOrEmptyException("The last name is required.");

        return new LastName(lastName);
    }

    public static implicit operator string?(LastName lastName)
    {
        return lastName.Value;
    }


    public static explicit operator LastName(string? lastName)
    {
        return Create(lastName);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value!;
    }
}