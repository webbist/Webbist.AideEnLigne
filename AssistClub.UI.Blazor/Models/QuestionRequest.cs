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
    public Guid UserId { get; set; } = Guid.Parse("85300a85-112e-4410-a2b4-1cc079584347"); //change this to the actual user id when authentication is implemented
    
    /// <summary>
    /// The title of the question.
    /// </summary>
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(TitleMaxLength, ErrorMessage = "Title must be at most {1} characters long.")]
    public string Title { get; set; }
    
    /// <summary>
    /// The full content of the question.
    /// </summary>
    [Required(ErrorMessage = "Content is required.")]
    [StringLength(ContentMaxLength, ErrorMessage = "Content must be at most {1} characters long.")]
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