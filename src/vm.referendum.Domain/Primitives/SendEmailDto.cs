using vm.referendum.Domain.ValueObjects;

namespace vm.referendum.Domain.Primitives;

public struct SendEmailDto
{
    public string Subject { get; set; }
    public Email To { get; set; }
    public string? Html { get; set; }
}