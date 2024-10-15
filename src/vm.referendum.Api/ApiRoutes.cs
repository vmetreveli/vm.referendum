namespace vm.referendum.Api;

public static class ApiRoutes
{
    public const string BASE_ROUTE = "api/v{version:apiVersion}/[controller]";

    public static class UserProfiles
    {
        public const string ID_ROUTE = "{id}";
    }

    public static class Questions
    {
        public const string All_Question = "all";
        public const string ID_ROUTE = "{id}";
        public const string ANSWERS = "{questionId}/answers";
        public const string ANSWER_BY_ID = "{questionId}/answers/{answerId}";
    }

    public static class Answers
    {
        public const string SET_ANSWER = "answer/";
        // public const string ANSWER_BY_ID = "{questionId}/answers/{answerId}";
    }

    public static class Authentication
    {
        public const string LOGIN = "login";
        public const string REGISTER = "registration";
        public const string IDENTITY_BY_ID = "{identityUserId}";
        public const string CURRENT_USER = "currentuser";
    }
}