namespace AssistClub.UI.Blazor.Models;

/// <summary>
/// Represents the request model for creating a question from the UI.
/// </summary>
public class QuestionRequest
{
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
    /// Defines the visibility of the question (<c>public</c> or <c>private</c>).
    /// </summary>
    public QuestionVisibility Visibility { get; set; }
    /// <summary>
    /// The status of the question (<c>open</c>, <c>pending</c>, or <c>resolved</c>).
    /// </summary>
    public string Status { get; set; }
}

public enum QuestionVisibility
{
    Public,
    Private
}