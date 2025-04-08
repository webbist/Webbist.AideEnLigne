namespace Domain.Enums;

/// <summary>
/// Represents the possible statuses of a question.
/// </summary>
public enum QuestionStatus
{
    /// <summary>
    /// Indicates that the question is open and accepting answers.
    /// </summary>
    Open,
    
    /// <summary>
    /// Indicates that the question has an answer that is pending review or approval.
    /// </summary>
    Pending,
    
    /// <summary>
    /// Indicates that the question has been answered and the answer is official.
    /// </summary>
    Resolved
}