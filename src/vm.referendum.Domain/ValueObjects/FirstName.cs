using vm.referendum.Domain.Exception;

namespace vm.referendum.Domain.ValueObjects;

public sealed class FirstName : ValueObject
{
    private FirstName()
    {
    }

    private FirstName(string value)
    {
        Value = value;
    }

    public string? Value { get; set; }

    public static FirstName Create(string? firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new NullOrEmptyException("The first name is required.");

        return new FirstName(firstName);
    }

    public static implicit operator string?(FirstName firstName)
    {
        return firstName.Value;
    }


    public static explicit operator FirstName(string? firstName)
    {
        return Create(firstName);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value!;
    }
}