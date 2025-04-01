namespace AssistClub.UI.Blazor.Routing;

/// <summary>
/// Defines the API endpoints used by the application for user-related operations. 
/// </summary>
public static class UserApiRouting
{
    /// <summary>
    /// API version used for routing.
    /// </summary>
    private const string Version = "v1";
    
    /// <summary>
    /// Base route for users.
    /// </summary>
    private const string Base = $"{Version}/user";
    
    /// <summary>
    /// Endpoint for retrieving all users.
    /// </summary>
    /// <param name="email">The email of the user to retrieve.</param>
    /// <returns>The API endpoint for retrieving a user by email.</returns>
    public static string GetUserByEmailRoute(string email) => $"{Base}/email/{email}";
    
    /// <summary>
    /// Endpoint for creating a new user.
    /// </summary>
    public const string CreateRoute = $"{Base}/create";
}