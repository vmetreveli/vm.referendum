using vm.referendum.Domain.Abstractions;

namespace vm.referendum.Domain.Exception.Question;

/// <summary>
///     Contains the domain errors.
/// </summary>
public static class QuestionErrors
{
    public static Error InvalidPermissions => new
    (
        "User.InvalidPermissions",
        "The current user does not have the permissions to perform that operation.");

    public static Error DuplicateEmail => new
        ("User.DuplicateEmail", "The specified email is already in use.");

    public static Error CannotChangePassword => new
    ("User.CannotChangePassword",
        "The password cannot be changed to the specified password.");

    public static Error NotFound(Guid id)
    {
        return new Error("User.NotFound", $"No Question found with ID = {id}");
    }

    public static Error NotFound()
    {
        return new Error("User.NotFound", "No Question found");
    }

    public static Error Validation()
    {
        return new Error("Question.Validation", "Question Exist");
    }
}