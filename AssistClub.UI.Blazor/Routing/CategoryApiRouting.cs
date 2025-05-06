namespace AssistClub.UI.Blazor.Routing;

/// <summary>
/// Defines the API endpoints used by the application for category-related operations.
/// </summary>
public class CategoryApiRouting
{
    /// <summary>
    /// API version used for routing.
    /// </summary>
    private const string Version = "v1";
    
    /// <summary>
    /// Base route for categories.
    /// </summary>
    private const string Base = $"{Version}/category";
    
    /// <summary>
    /// Endpoint for retrieving all categories.
    /// </summary>
    public const string GetAllRoute = $"{Base}";
}