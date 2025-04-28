using AssistClub.Application.DTOs;
using AssistClub.Application.Interfaces;
using Domain.Entities;

namespace AssistClub.Application.Services;

/// <summary>
/// Represents the service responsible for managing answer votes.
/// </summary>
/// <param name="answerVoteRepository">The repository responsible for answer vote management.</param>
public class AnswerVoteService(IAnswerVoteRepository answerVoteRepository) : IAnswerVoteService
{
    /// <summary>
    /// Toggles the vote for a specific answer by a user.
    /// </summary>
    /// <param name="request">The request containing the user ID and answer ID.</param>
    /// <returns>
    /// A <c>bool</c> indicating whether the vote was successfully toggled.
    /// </returns>
    public async Task<bool> ToggleVoteAsync(AnswerVoteRequest request)
    {
        var existingVote = await answerVoteRepository.GetVoteAsync(request.UserId, request.AnswerId);
        
        if (existingVote != null) return await answerVoteRepository.RemoveVoteAsync(existingVote);
        
        var newVote = new AnswerVote
        {
            Id = Guid.NewGuid(),
            AnswerId = request.AnswerId,
            UserId = request.UserId
        };
        return await answerVoteRepository.AddVoteAsync(newVote);
    }
}