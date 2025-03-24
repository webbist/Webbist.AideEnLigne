using System.ComponentModel.DataAnnotations;

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
    /// The unique identifier of the user submitting the question.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// The title of the question.
    /// </summary>
    [Required(ErrorMessageResourceName = "TitleRequiredMessage", ErrorMessageResourceType = typeof(Resources.AskQuestionResources))]
    [StringLength(TitleMaxLength, ErrorMessageResourceName = "TitleMaxLengthMessage", ErrorMessageResourceType = typeof(Resources.AskQuestionResources))]
    public string Title { get; set; }
    
    /// <summary>
    /// The full content of the question.
    /// </summary>
    [Required(ErrorMessageResourceName = "ContentRequiredMessage", ErrorMessageResourceType = typeof(Resources.AskQuestionResources))]
    [StringLength(ContentMaxLength, ErrorMessageResourceName = "ContentMaxLengthMessage", ErrorMessageResourceType = typeof(Resources.AskQuestionResources))]
    public string Content { get; set; }
    
    /// <summary>
    /// Defines the visibility of the question (<c>public</c> or <c>private</c>).
    /// </summary>
    public QuestionVisibility Visibility { get; set; } = QuestionVisibility.Public;
    
    /// <summary>
    /// The status of the question (<c>open</c>, <c>pending</c>, or <c>resolved</c>).
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