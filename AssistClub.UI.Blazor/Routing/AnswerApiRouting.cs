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
    /// <param name="userId">The unique identifier of the user to check if he voted for any answer.</param>
    public static string GetAllRoute(Guid userId) => $"{Base}/all/{userId}";
    
    /// <summary>
    /// Endpoint for updating the status of an answer.
    /// </summary>
    /// <param name="answerId">The unique identifier of the answer to be updated.</param>
    /// <returns>
    /// The route for updating the status of the specified answer.
    /// </returns>
    public static string UpdateStatusRoute(Guid answerId) => $"{Base}/status/{answerId}";
    
    /// <summary>
    /// Endpoint for updating an existing answer.
    /// </summary>
    /// <param name="answerId">The unique identifier of the answer to be updated.</param>
    /// <returns>
    /// The route for updating the specified answer.
    /// </returns>
    public static string UpdateRoute(Guid answerId) => $"{Base}/update/{answerId}";
    
    /// <summary>
    /// Endpoint for deleting an answer.
    /// </summary>
    /// <param name="answerId">The unique identifier of the answer to be deleted.</param>
    /// <returns>
    /// The route for deleting the specified answer.
    /// </returns>
    public static string DeleteRoute(Guid answerId) => $"{Base}/delete/{answerId}";
}