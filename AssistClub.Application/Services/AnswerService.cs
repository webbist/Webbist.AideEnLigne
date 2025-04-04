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
            throw new ArgumentException($"Answer content exceeds the maximum character limit of {AnswerRequest.ContentMaxLength}.");
        }
        
        var answer = new Answer
        {
            Id = Guid.NewGuid(),
            QuestionId = answerRequest.QuestionId,
            UserId = answerRequest.UserId,
            Content = answerRequest.Content,
            IsOfficial = false,
            CreatedAt = DateTime.UtcNow
        };
        
        var createdAnswer = await answerRepository.CreateAnswerAsync(answer);
        
        return new AnswerResponse
        {
            Id = createdAnswer.Id,
            QuestionId = createdAnswer.QuestionId,
            Content = createdAnswer.Content,
            IsOfficial = createdAnswer.IsOfficial,
            CreatedAt = createdAnswer.CreatedAt,
            UpdatedAt = createdAnswer.UpdatedAt
        };
    }

    /// <summary>
    /// Retrieves all answers in the system, including user information.
    /// </summary>
    /// <returns>
    /// An <see cref="IQueryable{T}"/> of <see cref="AnswerResponse"/> containing the answers.
    /// </returns>
    public async Task<IQueryable<AnswerResponse>> GetAnswersAsync()
    {
        var answers = await answerRepository.GetAnswers();
        return answers.Select(a => new AnswerResponse
            {
                Id = a.Id,
                QuestionId = a.QuestionId,
                User = new UserResponseDto
                {
                    Id = a.User.Id,
                    Firstname = a.User.Firstname,
                    Lastname = a.User.Lastname,
                    Email = a.User.Email,
                    Photo = a.User.Photo,
                    Club = a.User.Club,
                    Microsite = a.User.Microsite
                },
                Content = a.Content,
                IsOfficial = a.IsOfficial,
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt
            }
        ).AsQueryable();
    }

    /// <summary>
    /// Updates the official status of an answer and the associated question status.
    /// </summary>
    /// <param name="id">The unique identifier of the answer to be updated.</param>
    /// <param name="isOfficial">Indicates whether the answer is official or not.</param>
    /// <returns>
    /// Returns <c>true</c> if the update was successful; otherwise, <c>false</c>.
    /// </returns>
    public async Task<bool> UpdateAnswerOfficialStatusAsync(Guid id, bool isOfficial)
    {
        return await answerRepository.UpdateAnswerOfficialStatusAsync(id, isOfficial);
    }
}