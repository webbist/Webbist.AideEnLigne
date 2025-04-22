using System.ComponentModel.DataAnnotations;
using AssistClub.UI.Blazor.DataAnnotations;

namespace AssistClub.UI.Blazor.Models;

/// <summary>
/// Represents the request model for creating an answer from the UI.
/// </summary>
public class AnswerRequest
{
    /// <summary>
    /// Maximum allowed length for the content of the answer.
    /// </summary>
    public const int ContentMaxLength = 2000;
    
    /// <summary>
    /// Maximum allowed size for the attachment in bytes.
    /// </summary>
    public const int AttachmentMaxSize = 5 * 1024 * 1024;
    
    /// <summary>
    /// Gets or sets the unique identifier of the question to which the answer is being submitted.
    /// </summary>
    public Guid QuestionId { get; set; }
    
    /// <summary>
    /// Gets or sets the unique identifier of the user submitting the answer.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Gets or sets the full content of the answer.
    /// </summary>
    [Required(ErrorMessageResourceName = "ContentRequiredMessage", ErrorMessageResourceType = typeof(Resources.AnswerFormResources))]
    [HtmlRequired(ErrorMessageResourceName = "FormContentEmptyMessage", ErrorMessageResourceType = typeof(Resources.AnswerFormResources))]
    [StringLength(ContentMaxLength, ErrorMessageResourceName = "ContentMaxLengthMessage", ErrorMessageResourceType = typeof(Resources.AnswerFormResources))]
    public string Content { get; set; }
    
    /// <summary>
    /// Gets or sets the name of the file attached to the answer.
    /// </summary>
    public string? AttachmentName { get; set; }
}