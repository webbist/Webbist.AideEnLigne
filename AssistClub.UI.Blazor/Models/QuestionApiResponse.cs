using AssistClub.UI.Blazor.Extensions;

namespace AssistClub.UI.Blazor.Models;

/// <summary>
/// Represents the response model for a question from the API.
/// </summary>
public class QuestionApiResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the question.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the user who created the question.
    /// </summary>
    public UserApiResponse User { get; set; }
    
    /// <summary>
    /// Gets or sets the title of the question.
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Gets or sets the full content of the question.
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// Gets the plain text content of the question by stripping HTML tags.
    /// </summary>
    public string PlainTextContent => Content.GetPlainTextFromHtml();
    
    /// <summary>
    /// Gets or sets the date and time when the question was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the question was last updated.
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
    public string AttachmentName { get; set; }
    
    /// <summary>
    /// Gets or sets the list of categories associated with the question.
    /// </summary>
    public IEnumerable<string> Categories { get; set; }
}