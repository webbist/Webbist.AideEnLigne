namespace AssistClub.UI.Blazor.Models;

/// <summary>
/// Represents the response model for a question from the API.
/// </summary>
public class QuestionApiResponse
{
    /// <summary>
    /// The unique identifier of the question.
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// The unique identifier of the user submitting the question.
    /// </summary>
    public Guid UserId { get; set; }
    /// <summary>
    /// The title of the question.
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// The full content of the question.
    /// </summary>
    public string Content { get; set; }
    /// <summary>
    /// The date and time when the question was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// Defines the visibility of the question (<c>public</c> or <c>private</c>).
    /// </summary>
    public string Visibility { get; set; }
    /// <summary>
    /// The status of the question (<c>open</c>, <c>pending</c>, or <c>resolved</c>).
    /// </summary>
    public string Status { get; set; }
}