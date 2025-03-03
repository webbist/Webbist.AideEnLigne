using Domain.Entities;

namespace AssistClub.Application.Interfaces;

/// <summary>
/// Repository interface for managing question-related database operations.
/// This interface abstracts the data access layer, ensuring that business logic 
/// does not directly interact with the database and maintains the separation of concerns.
/// </summary>
public interface IQuestionRepository
{
    /// <summary>
    /// Adds a new question to the database.
    /// </summary>
    /// <param name="question"> The question entity to add.</param>
    /// <returns>The question entity that was added.</returns>
    Task<Question> CreateQuestionAsync(Question question);
    
    /// <summary>
    /// Retrieves a question by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the question.</param>
    /// <returns>The question if found, otherwise null.</returns>
    Task<Question?> GetQuestionByIdAsync(Guid id);
    
    /// <summary>
    /// Retrieves all questions in the database with optional visibility filter.
    /// </summary>
    /// <param name="visibility">The visibility filter to apply.</param>
    /// <returns>A list of all questions in the database.</returns>
    Task<List<Question>> GetAllQuestionsAsync(string? visibility = null);
    
    /// <summary>
    /// Updates an existing question in the database.
    /// </summary>
    /// <param name="question">The updated question entity.</param>
    /// <returns>True if the update was successful, otherwise false.</returns>
    Task<bool> UpdateQuestionAsync(Question question);
    
    /// <summary>
    /// Deletes a question from the database.
    /// </summary>
    /// <param name="id">The ID of the question to delete.</param>
    /// <returns>True if the deletion was successful, otherwise false.</returns>
    Task<bool> DeleteQuestionAsync(Guid id);
}