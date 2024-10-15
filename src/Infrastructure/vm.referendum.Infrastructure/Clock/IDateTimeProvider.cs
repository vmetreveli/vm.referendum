namespace vm.referendum.Infrastructure.Clock;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}