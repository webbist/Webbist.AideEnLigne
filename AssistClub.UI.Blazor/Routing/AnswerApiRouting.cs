namespace AssistClub.UI.Blazor.Routing;

/// <summary>
/// Defines the API endpoints used by the application for answer-related operations.
/// </summary>
public static class AnswerApiRouting
{
    /// <summary>
    /// API version used for routing.
    /// </summary>
    private const string Version = "v1";
    
    /// <summary>
    /// Base route for answers.
    /// </summary>
    private const string Base = $"{Version}/answer";
    
    /// <summary>
    /// Endpoint for creating a new answer.
    /// </summary>
    public const string CreateRoute = $"{Base}/create";
    
    /// <summary>
    /// Endpoint for retrieving all answers.
    /// </summary>
    public const string GetAllRoute = $"{Base}/all";
}