using AssistClub.Application.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace AssistClub.Application.Interfaces;

/// <summary>
/// Represents the repository interface for managing question-related database operations.
/// </summary>
/// <remarks>
/// Abstracts the data access layer to maintain separation of concerns 
/// and prevent direct interaction between business logic and the database.
/// </remarks>
public interface IQuestionRepository
{
    /// <summary>
    /// Adds a new question to the database.
    /// </summary>
    /// <param name="question">The <see cref="Question"/> entity to add.</param>
    /// <returns>The <see cref="Question"/> entity that was added.</returns>
    Task<Question> CreateQuestionAsync(Question question);
    
    /// <summary>
    /// Retrieves a question by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the question.</param>
    /// <returns>The <see cref="Question"/> entity if found; otherwise, <c>null</c>.</returns>
    Task<Question?> GetQuestionByIdAsync(Guid id);
    
    /// <summary>
    /// Retrieves all questions from the database, allowing further filtering, sorting, and pagination.
    /// </summary>
    /// <returns>An <see cref="IQueryable{T}"/> representing the questions in the database.</returns>
    Task<IQueryable<Question>> GetQuestions();
    
    /// <summary>
    /// Updates an existing question in the database.
    /// </summary>
    /// <param name="question">The updated <see cref="Question"/> entity.</param>
    /// <returns><c>true</c> if the update was successful; otherwise, <c>false</c>.</returns>
    Task<bool> UpdateQuestionAsync(Question question);
    
    /// <summary>
    /// Deletes a question from the database.
    /// </summary>
    /// <param name="id">The ID of the question to delete.</param>
    /// <returns><c>true</c> if the deletion was successful; otherwise, <c>false</c>.</returns>
    Task<bool> DeleteQuestionAsync(Guid id);
}