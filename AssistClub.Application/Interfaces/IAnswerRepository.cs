using Domain.Entities;

namespace AssistClub.Application.Interfaces;

/// <summary>
/// Represents the repository interface for managing answer-related database operations.
/// </summary>
/// <remarks>
/// Abstracts the data access layer to maintain separation of concerns
/// and prevent direct interaction between business logic and the database.
/// </remarks>
public interface IAnswerRepository
{
    /// <summary>
    /// Adds a new answer to the database and updates the status of the associated question.
    /// </summary>
    /// <param name="answer">The <see cref="Answer"/> entity to add.</param>
    /// <returns>The <see cref="Answer"/> entity that was added.</returns>
    Task<Answer> CreateAnswerAsync(Answer answer);
    
    /// <summary>
    /// Retrieves all answers from the database, allowing further filtering, sorting, and pagination.
    /// </summary>
    /// <returns>
    /// An <see cref="IQueryable{T}"/> representing the answers in the database.
    /// </returns>
    Task<IQueryable<Answer>> GetAnswers();

    /// <summary>
    /// Updates the official status of an answer and the associated question status.
    /// </summary>
    /// <param name="answerId">The unique identifier of the answer to be updated.</param>
    /// <param name="isOfficial">Indicates whether the answer is official or not.</param>
    /// <returns>
    /// Returns <c>true</c> if the update was successful; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> UpdateAnswerOfficialStatusAsync(Guid answerId, bool isOfficial);
}