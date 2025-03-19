namespace AssistClub.UI.Blazor.Constants;

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
    }
    
    /// <summary>
    /// Base route for users.
    /// </summary>
    public static class Users
    {
        private const string Base = $"{Version}/user";
        public static string GetUserByEmail(string email) => $"{Base}/{email}";
    }
}