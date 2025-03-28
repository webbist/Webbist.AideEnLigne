namespace AssistClub.UI.Blazor.Routing;

/// <summary>
/// Defines the API endpoints used by the application.
/// </summary>
public static class ApiRoutes
{
    private const string Version = "v1";
    
    /// <summary>
    /// Base route for questions.
    /// </summary>
    public static class Questions
    {
        private const string Base = $"{Version}/question";
        public const string Create = $"{Base}/create";
        public const string GetAll = $"{Base}/all";
        public static string GetById(Guid id) => $"{Base}/{id}";
    }
    
    /// <summary>
    /// Base route for users.
    /// </summary>
    public static class Users
    {
        private const string Base = $"{Version}/user";
        public static string GetUserByEmail(string email) => $"{Base}/{email}";
        public const string CreateUser = $"{Base}/create";
    }
    
    /// <summary>
    /// Base route for answers.
    /// </summary>
    public static class Answers
    {
        private const string Base = $"{Version}/answer";
        public const string Create = $"{Base}/create";
        public const string GetAll = $"{Base}/all";
    }
}