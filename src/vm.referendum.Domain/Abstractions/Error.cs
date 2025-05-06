namespace vm.referendum.Domain.Abstractions;

public record Error(string Code, string Name)
{
    public string Code { get; } = Code ?? throw new ArgumentNullException(nameof(Code));
    public string Name { get; } = Name ?? throw new ArgumentNullException(nameof(Name));
}