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
        byte[] randomBytes = new byte[passwordLength];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        StringBuilder password = new(passwordLength);

        foreach (byte b in randomBytes)
        {
            password.Append(AllowedChars[b % AllowedChars.Length]);
        }

        return password.ToString();
    }
}