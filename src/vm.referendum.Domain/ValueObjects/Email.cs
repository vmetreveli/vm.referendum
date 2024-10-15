using System.Text.RegularExpressions;
using Framework.Infrastructure.Exceptions;
using vm.referendum.Domain.Errors;

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
            //   return Result.Failure<Email>(EmailErrors.NullOrEmpty);
            throw new ObjectNotFoundException(typeof(Email).ToString(), EmailErrors.NullOrEmpty.Name);


        var trimMail = email.Trim();

        if (trimMail.Length > 150)
            //return Result.Failure<Email>(EmailErrors.LongerThanAllowed);
            throw new ObjectNotFoundException(typeof(Email).ToString(), EmailErrors.LongerThanAllowed.Name);

        var isValid = Regex.IsMatch(trimMail, @"^(.+)@(.+)$");

        if (!isValid)
            // return Result.Failure<Email>(EmailErrors.InvalidFormat);
            throw new ObjectNotFoundException(typeof(Email).ToString(), EmailErrors.InvalidFormat.Name);

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