namespace AssistClub.UI.Blazor.Components.Enums;

/// <summary>
/// Represents the status of an answer.
/// </summary>
public enum AnswerStatus
{
    /// <summary>
    /// Indicates that the answer is pending review.
    /// </summary>
    Pending,
    
    /// <summary>
    /// Indicates that the answer is official and has been approved.
    /// </summary>
    Official,
    
    /// <summary>
    /// Indicates that the answer has been archived and is no longer active.
    /// </summary>
    Archived
}