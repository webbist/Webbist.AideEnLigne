using AssistClub.Application.DTOs;

namespace AssistClub.Application.Interfaces;

/// <summary>
/// Represents the service interface for handling business logic related to answer votes.
/// </summary>
public interface IAnswerVoteService
{
    /// <summary>
    /// Toggles the vote for a specific answer by a user.
    /// </summary>
    /// <param name="request">The request containing the user ID and answer ID.</param>
    /// <returns>
    /// A <c>bool</c> indicating whether the vote was successfully toggled.
    /// </returns>
    Task<bool> ToggleVoteAsync(AnswerVoteRequest request);
}