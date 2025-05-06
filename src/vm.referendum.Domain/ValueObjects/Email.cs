using System.Text.RegularExpressions;
using vm.referendum.Domain.Exception.Email;


namespace vm.referendum.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    private Email()
    {
    }

    private Email(string value)
    {
        Value = value;
    }

    public string? Value { get; }

    public static Email Create(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new EmailException(EmailErrors.NullOrEmpty.Code, EmailErrors.NullOrEmpty.Name);


        string trimMail = email.Trim();

        if (trimMail.Length > 150)
            throw new EmailException(EmailErrors.LongerThanAllowed.Code, EmailErrors.LongerThanAllowed.Name);

        bool isValid = Regex.IsMatch(trimMail, @"^(.+)@(.+)$");

        if (!isValid)
            throw new EmailException(EmailErrors.InvalidFormat.Code, EmailErrors.InvalidFormat.Name);

        return new Email(email);
    }

    public static implicit operator string?(Email email)
    {
        return email.Value;
    }


    public static explicit operator Email(string? email)
    {
        return Create(email);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value!;
    }
}