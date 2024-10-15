namespace vm.referendum.Infrastructure.Authentication.Cryptography;

public interface IPasswordGenerator
{
    string GeneratePassword();
}