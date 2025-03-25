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
    /// Gets or sets the unique identifier of the user submitting the question.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Gets or sets the title of the question.
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Gets or sets the full content of the question.
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// Gets or sets the visibility of the question (<c>public</c> or <c>private</c>).
    /// </summary>
    public QuestionVisibility Visibility { get; set; }
    
    /// <summary>
    /// Gets or sets the status of the question (<c>open</c>, <c>pending</c>, or <c>resolved</c>).
    /// </summary>
    public string Status { get; set; }
}