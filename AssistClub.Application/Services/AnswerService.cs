using AssistClub.Application.DTOs;
using AssistClub.Application.Interfaces;
using Domain.Entities;

namespace AssistClub.Application.Services;

/// <inheritdoc/>
public class AnswerService(IAnswerRepository answerRepository): IAnswerService
{
    /// <summary>
    /// Creates a new answer in the system.
    /// </summary>
    /// <remarks>
    /// Validates the input and delegates the persistence to the repository.
    /// Returns a simplified view model for external use.
    /// </remarks>
    /// <param name="answerRequest">The <see cref="AnswerRequest"/> containing answer details.</param>
    /// <returns>A <see cref="AnswerResponse"/> representing the created answer.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown if the answer content exceeds <see cref="AnswerRequest.ContentMaxLength"/> characters.
    /// </exception>
    public async Task<AnswerResponse> CreateAnswerAsync(AnswerRequest answerRequest)
    {
        if (answerRequest.Content.Length > AnswerRequest.ContentMaxLength)
        {
            throw new ArgumentException("Answer content exceeds the maximum character limit of 2000.");
        }
        
        var answer = new Answer
        {
            Id = Guid.NewGuid(),
            QuestionId = answerRequest.QuestionId,
            UserId = answerRequest.UserId,
            Content = answerRequest.Content,
            CreatedAt = DateTime.UtcNow
        };
        
        var createdAnswer = await answerRepository.CreateAnswerAsync(answer);
        
        return new AnswerResponse
        {
            Id = createdAnswer.Id,
            QuestionId = createdAnswer.QuestionId,
            Content = createdAnswer.Content,
            CreatedAt = createdAnswer.CreatedAt,
            UpdatedAt = createdAnswer.UpdatedAt
        };
    }
}