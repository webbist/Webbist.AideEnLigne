using Domain.Enums;

namespace AssistClub.Application.DTOs;

/// <summary>
/// Represents the request structure for creating or updating a question.
/// </summary>
/// <remarks>
/// Ensures that only valid and necessary data is received from the client before processing.
/// </remarks>
public class QuestionRequestDto
{
    /// <summary>
    /// Maximum allowed length for the title.
    /// </summary>
    public const int TitleMaxLength = 255;

    /// <summary>
    /// Maximum allowed length for the content.
    /// </summary>
    public const int ContentMaxLength = 2000;
    
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