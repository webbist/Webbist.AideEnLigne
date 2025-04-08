namespace AssistClub.UI.Blazor.Components.Enums;

/// <summary>
/// Represents the visibility of a question (<c>Public</c> or <c>Private</c>).
/// </summary>
public enum QuestionVisibility
{
    /// <summary>
    /// Indicates that the question is visible to all users.
    /// </summary>
    Public,
    
    /// <summary>
    /// Indicates that the question is only visible to the user who created it and the admin.
    /// </summary>
    Private
}