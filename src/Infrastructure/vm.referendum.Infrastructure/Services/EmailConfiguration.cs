namespace vm.referendum.Infrastructure.Services;

public class EmailConfiguration
{
    public const string SETTINGS_KEY = "EmailConfiguration";
    public string From { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string Password { get; set; }
}