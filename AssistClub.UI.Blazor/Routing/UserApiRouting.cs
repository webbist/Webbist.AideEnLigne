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
    /// Endpoint for retrieving a user by email.
    /// </summary>
    /// <param name="email">The email of the user to retrieve.</param>
    /// <returns>The route to get a user by email.</returns>
    public static string GetUserByEmailRoute(string email) => $"{Base}/{email}";
    
    /// <summary>
    /// Endpoint for creating a new user.
    /// </summary>
    public const string CreateRoute = $"{Base}/create";
    
    /// <summary>
    /// Endpoint for retrieving a user by ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>The route to get a user by ID.</returns>
    public static string GetUserByIdRoute(Guid id) => $"{Base}/{id}";
}