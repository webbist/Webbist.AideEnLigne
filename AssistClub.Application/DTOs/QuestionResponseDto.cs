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
    /// Gets or sets the unique identifier of the question.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the user who created the question.
    /// </summary>
    public UserResponseDto User { get; set; }
    
    /// <summary>
    /// Gets or sets the title of the question.
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Gets or sets the full content of the question.
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp indicating when the question was created.
    /// </summary>
    public DateTime? CreatedAt { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp indicating when the question was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
    
    /// <summary>
    /// Gets or sets the visibility of the question (<c>public</c> or <c>private</c>).
    /// </summary>
    public string Visibility { get; set; }
    
    /// <summary>
    /// Gets or sets the status of the question (<c>open</c>, <c>pending</c>, or <c>resolved</c>).
    /// </summary>
    public string Status { get; set; }
    
    /// <summary>
    /// Gets or sets the name of the uploaded attachment file.
    /// </summary>
    public string? AttachmentName { get; set; }
    
    /// <summary>
    /// Gets or sets the list of categories associated with the question.
    /// </summary>
    public IEnumerable<string> Categories { get; set; }
}