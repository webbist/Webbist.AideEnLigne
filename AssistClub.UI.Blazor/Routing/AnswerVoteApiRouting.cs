namespace AssistClub.UI.Blazor.Routing;

/// <summary>
/// Defines the API endpoints used by the application for answer vote-related operations.
/// </summary>
public class AnswerVoteApiRouting
{
    /// <summary>
    /// API version used for routing.
    /// </summary>
    private const string Version = "v1";
    
    /// <summary>
    /// Base route for answer votes.
    /// </summary>
    private const string Base = $"{Version}/answervote";
    
    /// <summary>
    /// Endpoint for toggling a vote on an answer.
    /// </summary>
    public const string ToggleVoteRoute = $"{Base}/toggle";
}