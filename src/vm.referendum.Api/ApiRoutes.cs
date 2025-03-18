namespace vm.referendum.Api;

public static class ApiRoutes
{
    public const string BaseRoute = "api/v{version:apiVersion}/[controller]";

    public static class UserProfiles
    {
        public const string IdRoute = "{id}";
    }

    public static class Questions
    {
        public const string All = "all";
        public const string Id = "{id}";
        public const string Answers = "{questionId}/answers";
        public const string AnswerById = "{questionId}/answers/{answerId}";
    }

    public static class Answers
    {
        public const string Set = "answer/";
    }

    public static class Authentication
    {
        public const string Login = "login";
        public const string Register = "registration";
        public const string Identity = "{identityUserId}";
        public const string Current = "currentuser";
    }
}