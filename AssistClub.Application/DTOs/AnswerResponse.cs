namespace AssistClub.Application.DTOs;

/// <summary>
/// Represents the response structure for an answer.
/// </summary>
/// <remarks>
/// Ensures that only relevant answer details are exposed to the client.
/// </remarks>
public class AnswerResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the answer.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the unique identifier of the question being answered.
    /// </summary>
    public Guid QuestionId { get; set; }
    
    /// <summary>
    /// Gets or sets the user who created the answer.
    /// </summary>
    public UserResponseDto User { get; set; }
    
    /// <summary>
    /// Gets or sets the full content of the answer.
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the answer is official.
    /// </summary>
    public bool IsOfficial { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp indicating when the answer was created.
    /// </summary>
    public DateTime? CreatedAt { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp indicating when the answer was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}