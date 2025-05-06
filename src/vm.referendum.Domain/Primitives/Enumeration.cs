using System.Reflection;

namespace vm.referendum.Domain.Primitives;

public abstract class Enumeration<TEnum>(Guid value, string name) : IEquatable<Enumeration<TEnum>>
    where TEnum : Enumeration<TEnum>
{
    private static readonly Dictionary<Guid, TEnum> _enumerations = CreateEnumerations();

    protected Enumeration() : this(default, string.Empty)
    {
    }

    public Guid Value { get; protected init; } = value;

    public string Name { get; protected init; } = name;

    public bool Equals(Enumeration<TEnum>? other)
    {
        if (other is null) return false;

        return GetType() == other.GetType() && other.Value.Equals(Value);
    }

    public int CompareTo(Enumeration<TEnum>? other)
    {
        return other is null ? 1 : Value.CompareTo(other.Value);
    }

    public static TEnum? FromValue(Guid value)
    {
        return _enumerations.GetValueOrDefault(value);
    }

    public static TEnum? FromName(string name)
    {
        return _enumerations.Values.SingleOrDefault(e => e.Name == name);
    }


    public static bool operator ==(Enumeration<TEnum>? a, Enumeration<TEnum>? b)
    {
        if (a is null && b is null) return true;

        if (a is null || b is null) return false;

        return a.Equals(b);
    }

    public static bool operator !=(Enumeration<TEnum>? a, Enumeration<TEnum>? b)
    {
        return !(a == b);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;

        if (!(obj is Enumeration<TEnum> otherValue)) return false;

        return GetType() == obj.GetType() && otherValue.Value.Equals(Value);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }


    private static Dictionary<Guid, TEnum> CreateEnumerations()
    {
        Type enumerationType = typeof(TEnum);
        IEnumerable<TEnum> fieldForType = enumerationType.GetFields(
                BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fieldInfo => enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo => (TEnum)fieldInfo.GetValue(default)!);
        return fieldForType.ToDictionary(x => x.Value);
    }

    public static IReadOnlyCollection<TEnum> GetValues()
    {
        return _enumerations.Values.ToList();
    }
}