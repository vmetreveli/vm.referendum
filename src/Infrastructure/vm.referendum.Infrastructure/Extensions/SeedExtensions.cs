using vm.referendum.Domain.Entities;
using vm.referendum.Infrastructure.Context;

namespace vm.referendum.Infrastructure.Extensions;

/// <summary>
///     Contains the extension method for seeding the database with initial data.
/// </summary>
public static class SeedExtensions
{
    /// <summary>
    ///     Seeds the database with initial data.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public static void SeedDatabase(this DataContext dbContext)
    {
        dbContext.Set<Question>().AddRange(new List<Question>
        {
            new(Guid.Parse("17dbd29b-0fca-444e-ba20-f3f5a35dc59d"), "BMW"),
            new(Guid.Parse("7230a579-67c7-4713-905a-467c26b7d3b4"), "Audi"),
            new(Guid.Parse("db24dbdb-b490-466a-a211-32cb081fdc30"), "Benz")
        });


        dbContext.Set<Answer>().AddRange(new List<Answer>
        {
            new("Yes", Guid.Parse("17dbd29b-0fca-444e-ba20-f3f5a35dc59d")),
            new("No", Guid.Parse("17dbd29b-0fca-444e-ba20-f3f5a35dc59d"))
        });


        dbContext.SaveChanges();
    }
}