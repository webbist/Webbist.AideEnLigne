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
    /// Adds a new answer to the database.
    /// </summary>
    /// <param name="answer">The <see cref="Answer"/> entity to add.</param>
    /// <returns>The <see cref="Answer"/> entity that was added.</returns>
    Task<Answer> CreateAnswerAsync(Answer answer);
}