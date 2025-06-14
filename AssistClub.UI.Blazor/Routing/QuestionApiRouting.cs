namespace AssistClub.UI.Blazor.Routing;

/// <summary>
/// Defines the API endpoints used by the application for question-related operations.
/// </summary>
public static class QuestionApiRouting
{
    /// <summary>
    /// API version used for routing.
    /// </summary>
    private const string Version = "v1";
    
    /// <summary>
    /// Base route for questions.
    /// </summary>
    private const string Base = $"{Version}/question";
    
    /// <summary>
    /// Endpoint for creating a new question.
    /// </summary>
    public const string CreateRoute = $"{Base}/create";
    
    /// <summary>
    /// Endpoint for retrieving all questions.
    /// </summary>
    public const string GetAllRoute = $"{Base}/all";
    
    /// <summary>
    /// Endpoint for retrieving a question by its ID.
    /// </summary>
    /// <param name="id">The ID of the question.</param>
    /// <returns>The full URL for the question with the specified ID.</returns>
    public static string GetByIdRoute(Guid id) => $"{Base}/{id}";
    
    /// <summary>
    /// Endpoint for updating a question by its ID.
    /// </summary>
    /// <param name="id">The ID of the question.</param>
    /// <returns>The full URL for the question update endpoint.</returns>
    public static string UpdateRoute(Guid id) => $"{Base}/update/{id}";
    
    /// <summary>
    /// Endpoint for deleting a question by its ID.
    /// </summary>
    /// <param name="questionId">The ID of the question to be deleted.</param>
    /// <returns>
    /// The full URL for the question deletion endpoint.
    /// </returns>
    public static string DeleteRoute(Guid questionId) => $"{Base}/delete/{questionId}";
}