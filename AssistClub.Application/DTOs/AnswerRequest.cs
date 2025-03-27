namespace AssistClub.Application.DTOs;

/// <summary>
/// Represents the request structure for creating a new answer.
/// </summary>
/// <remarks>
/// Ensures that only valid and necessary data is received from the client before processing.
/// </remarks>
public class AnswerRequest
{
    /// <summary>
    /// Maximum allowed length for the content.
    /// </summary>
    public const int ContentMaxLength = 2000;
    
    /// <summary>
    /// Gets or sets the unique identifier of the question being answered.
    /// </summary>
    public Guid QuestionId { get; set; }
    
    /// <summary>
    /// Gets or sets the unique identifier of the user submitting the answer.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Gets or sets the full content of the answer.
    /// </summary>
    public string Content { get; set; }
}