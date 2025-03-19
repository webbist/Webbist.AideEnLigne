namespace AssistClub.Application.DTOs;

/// <summary>
/// Represents the response structure for a question.
/// </summary>
/// <remarks>
/// Ensures that only relevant question details are exposed to the client.
/// </remarks>
public class QuestionResponseDto
{
    /// <summary>
    /// The user who created the question.
    /// </summary>
    public UserResponseDto User { get; set; }
    
    /// <summary>
    /// The title of the question.
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// The full content of the question.
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// The timestamp indicating when the question was created.
    /// </summary>
    public DateTime? CreatedAt { get; set; }
    
    /// <summary>
    /// The timestamp indicating when the question was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
    
    /// <summary>
    /// Defines the visibility of the question (<c>public</c> or <c>private</c>).
    /// </summary>
    public string Visibility { get; set; }
    
    /// <summary>
    /// The status of the question (<c>open</c>, <c>pending</c>, or <c>resolved</c>).
    /// </summary>
    public string Status { get; set; }
}