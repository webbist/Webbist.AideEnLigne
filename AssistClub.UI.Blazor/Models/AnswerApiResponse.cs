namespace AssistClub.UI.Blazor.Models;

/// <summary>
/// Represents the response model for an answer from the API.
/// </summary>
public class AnswerApiResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the answer.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the unique identifier of the question to which the answer belongs.
    /// </summary>
    public Guid QuestionId { get; set; }
    
    /// <summary>
    /// Gets or sets the user who submitted the answer.
    /// </summary>
    public UserApiResponse User { get; set; }
    
    /// <summary>
    /// Gets or sets the full content of the answer.
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// Gets or sets the status of the answer, indicating whether it is <c>Official</c>, <c>Pending</c> or <c>Archived</c>.
    /// </summary>
    public string Status { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time the answer was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time the answer was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
    
    /// <summary>
    /// Gets or sets the name of the file attached to the answer.
    /// </summary>
    public string? AttachmentName { get; set; }
}