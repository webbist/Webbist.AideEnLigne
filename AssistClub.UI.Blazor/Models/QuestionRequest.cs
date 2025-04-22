using System.ComponentModel.DataAnnotations;
using AssistClub.UI.Blazor.Components.Enums;
using AssistClub.UI.Blazor.DataAnnotations;

namespace AssistClub.UI.Blazor.Models;

/// <summary>
/// Represents the request model for creating a question from the UI.
/// </summary>
public class QuestionRequest
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
    /// Maximum allowed size for the attachment in bytes.
    /// </summary>
    public const int AttachmentMaxSize = 5 * 1024 * 1024;
    
    /// <summary>
    /// Gets or sets the unique identifier of the user submitting the question.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Gets or sets the title of the question.
    /// </summary>
    [Required(ErrorMessageResourceName = "TitleRequiredMessage", ErrorMessageResourceType = typeof(Resources.AskQuestionResources))]
    [StringLength(TitleMaxLength, ErrorMessageResourceName = "TitleMaxLengthMessage", ErrorMessageResourceType = typeof(Resources.AskQuestionResources))]
    public string Title { get; set; }
    
    /// <summary>
    /// Gets or sets the full content of the question.
    /// </summary>
    [Required(ErrorMessageResourceName = "ContentRequiredMessage", ErrorMessageResourceType = typeof(Resources.AskQuestionResources))]
    [HtmlRequired(ErrorMessageResourceName = "ContentRequiredMessage", ErrorMessageResourceType = typeof(Resources.AskQuestionResources))]
    [StringLength(ContentMaxLength, ErrorMessageResourceName = "ContentMaxLengthMessage", ErrorMessageResourceType = typeof(Resources.AskQuestionResources))]
    public string Content { get; set; }
    
    /// <summary>
    /// Gets or sets the visibility of the question (<c>public</c> or <c>private</c>).
    /// </summary>
    public QuestionVisibility Visibility { get; set; } = QuestionVisibility.Public;
    
    /// <summary>
    /// Gets or sets the status of the question (<c>open</c>, <c>pending</c>, or <c>resolved</c>).
    /// </summary>
    public string Status { get; set; } = QuestionStatus.Open.ToString();
    
    /// <summary>
    /// Gets or sets the name of the uploaded attachment file.
    /// </summary>
    public string? AttachmentName { get; set; }
}