using AssistClub.Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AssistClub.Infrastructure.Persistence.Repositories;

/// <inheritdoc/>
public class QuestionRepository(AssistClubDbContext db, ILogger<QuestionRepository> logger) : IQuestionRepository
{
    /// <summary>
    /// Adds a new question to the database.
    /// </summary>
    /// <param name="question">The <see cref="Question"/> entity to add.</param>
    /// <returns>The <see cref="Question"/> entity that was added.</returns>
    /// <exception cref="DbUpdateException">Thrown if an error occurs while saving the question to the database.</exception>
    public async Task<Question> CreateQuestionAsync(Question question)
    {
        try
        {
            var result = db.Questions.Add(question);
            await db.SaveChangesAsync();
            return result.Entity;
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "An error occurred while adding a new question to the database.");
            throw;
        }
    }

    /// <summary>
    /// Retrieves a question by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the question.</param>
    /// <returns>The <see cref="Question"/> entity if found; otherwise, <c>null</c>.</returns>
    public async Task<Question?> GetQuestionByIdAsync(Guid id)
    {
        return await db.Questions.FindAsync(id);
    }

    /// <summary>
    /// Retrieves all questions in the database.
    /// </summary>
    /// <returns>A list of all <see cref="Question"/> entities.</returns>
    public async Task<List<Question>> GetAllQuestionsAsync()
    {
        return await db.Questions.ToListAsync();
    }

    /// <summary>
    /// Retrieves all questions in the database filtered by visibility.
    /// </summary>
    /// <param name="visibility">The visibility filter <see cref="QuestionVisibility"/>.</param>
    /// <returns>A list of <see cref="Question"/> entities matching the criteria.</returns>
    public async Task<List<Question>> GetAllQuestionsAsync(QuestionVisibility visibility)
    {
        return await db.Questions
            .Where(q => q.Visibility == visibility.ToString())
            .ToListAsync();
    }

    /// <summary>
    /// Updates an existing question in the database.
    /// </summary>
    /// <param name="question">The updated <see cref="Question"/> entity.</param>
    /// <returns><c>true</c> if the update was successful; otherwise, <c>false</c>.</returns>
    public async Task<bool> UpdateQuestionAsync(Question question)
    {
        db.Questions.Update(question);
        return await db.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Deletes a question from the database.
    /// </summary>
    /// <param name="id">The ID of the question to delete.</param>
    /// <returns><c>true</c> if the deletion was successful; otherwise, <c>false</c>.</returns>
    public async Task<bool> DeleteQuestionAsync(Guid id)
    {
        var question = await db.Questions.FindAsync(id);
        if (question == null)
        {
            logger.LogInformation("Delete request for question with ID {Id}, but it does not exist.", id);
            return true;
        }
        db.Questions.Remove(question);
        return await db.SaveChangesAsync() == 1;
    }
}