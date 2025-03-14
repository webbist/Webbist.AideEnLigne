namespace Domain.Enums;

/// <summary>
/// Represents the possible visibility states of a question.
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