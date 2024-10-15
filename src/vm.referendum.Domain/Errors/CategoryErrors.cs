using vm.referendum.Domain.Abstractions;

namespace vm.referendum.Domain.Errors;

/// <summary>
///     Contains the domain errors.
/// </summary>
public static class CategoryErrors
{
    public static Error CannotChangePassword => new
    ("User.CannotChangePassword",
        "The password cannot be changed to the specified password.");

    public static Error NotFound(Guid id)
    {
        return new Error("Category.NotFound", $"No Category found with ID = {id}");
    }

    public static Error NotFound()
    {
        return new Error("Category.NotFound", "No Category found");
    }
}