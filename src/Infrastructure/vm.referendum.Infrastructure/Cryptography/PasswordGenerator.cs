using System.Security.Cryptography;
using System.Text;
using vm.referendum.Infrastructure.Authentication.Cryptography;

namespace vm.referendum.Infrastructure.Cryptography;

public class PasswordGenerator : IPasswordGenerator
{
    private const string AllowedChars =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_-+=<>?";

    private readonly int passwordLength = 10;

    public string GeneratePassword()
    {
        var randomBytes = new byte[passwordLength];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        var password = new StringBuilder(passwordLength);

        foreach (var b in randomBytes) password.Append(AllowedChars[b % AllowedChars.Length]);

        return password.ToString();
    }
}