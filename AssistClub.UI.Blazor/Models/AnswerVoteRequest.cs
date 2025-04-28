namespace AssistClub.UI.Blazor.Models;

/// <summary>
/// Represents a request to vote on an answer.
/// </summary>
public class AnswerVoteRequest
{
    /// <summary>
    /// The unique identifier of the answer being voted on.
    /// </summary>
    public Guid AnswerId { get; set; }
    
    /// <summary>
    /// The unique identifier of the user casting the vote.
    /// </summary>
    public Guid UserId { get; set; }
}