using Framework.Abstractions.Exceptions;

namespace vm.referendum.Domain.ValueObjects;

public sealed class Name : ValueObject
{
    private Name()
    {
    }

    private Name(string value)
    {
        Value = value;
    }

    public string? Value { get; set; }

    public static Name Create(string? Name)
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new InflowException("Name is required");

        return new Name(Name);
    }

    public static implicit operator string?(Name name)
    {
        return name.Value;
    }


    public static explicit operator Name(string? name)
    {
        return Create(name);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value!;
    }
}