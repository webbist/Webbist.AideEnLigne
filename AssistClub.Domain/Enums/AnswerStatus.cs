namespace Domain.Enums;

/// <summary>
/// Represents the possible statuses of an answer.
/// </summary>
public enum AnswerStatus
{
    /// <summary>
    /// Indicates that the answer is pending review or approval.
    /// </summary>
    Pending,
    
    /// <summary>
    /// Indicates that the answer has been approved and is now official.
    /// </summary>
    Official,
    
    /// <summary>
    /// Indicates that the answer has been rejected or is no longer valid.
    /// </summary>
    Archived
}