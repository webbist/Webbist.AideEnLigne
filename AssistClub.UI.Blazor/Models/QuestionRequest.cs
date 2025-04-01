using System.ComponentModel.DataAnnotations;
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
    public string Status { get; set; } = "open";
}

/// <summary>
/// Represents the visibility of a question (<c>Public</c> or <c>Private</c>).
/// </summary>
public enum QuestionVisibility
{
    Public,
    Private
}