using AssistClub.Application.Interfaces;
using Domain.Entities;
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
    /// Retrieves a question by its unique identifier including the user who submitted it.
    /// </summary>
    /// <param name="id">The ID of the question.</param>
    /// <returns>The <see cref="Question"/> entity if found; otherwise, <c>null</c>.</returns>
    /// <exception cref="InvalidOperationException">Thrown if multiple questions are found with the same ID.</exception>
    public async Task<Question?> GetQuestionByIdAsync(Guid id)
    {
        try
        {
            return await db.Questions
                .Include(q => q.User)
                .Include(q => q.Categories)
                .SingleOrDefaultAsync(q => q.Id == id);
        }
        catch (InvalidOperationException ex)
        {
            logger.LogError(ex, "An error occurred while getting a question.");
            throw new InvalidOperationException("Data integrity issue: multiple questions found with the same ID.", ex);
        }
    }

    /// <summary>
    /// Retrieves all questions from the database, allowing further filtering, sorting, and pagination.
    /// </summary>
    /// <returns>An <see cref="IQueryable{T}"/> representing the questions in the database.</returns>
    public async Task<IQueryable<Question>> GetQuestions()
    {
        return await Task.FromResult(db.Questions);
    }

    /// <summary>
    /// Updates an existing question in the database.
    /// </summary>
    /// <param name="question">The updated <see cref="Question"/> entity.</param>
    /// <returns><c>true</c> if the update was successful; otherwise, <c>false</c>.</returns>
    public async Task<bool> UpdateQuestionAsync(Question question)
    {
        try
        {
            var existingQuestion = await db.Questions.FindAsync(question.Id);
            if (existingQuestion == null) return false;
            existingQuestion.UserId = question.UserId;
            existingQuestion.Title = question.Title;
            existingQuestion.Content = question.Content;
            existingQuestion.UpdatedAt = question.UpdatedAt;
            existingQuestion.Visibility = question.Visibility;
            existingQuestion.AttachmentName = question.AttachmentName;
            existingQuestion.ModifiedBy = question.ModifiedBy;
            db.Questions.Update(existingQuestion);
            return await db.SaveChangesAsync() > 0;
        }
        catch (DbUpdateException e)
        {
            logger.LogError(e, "An error occurred while updating the question.");
            throw;
        }
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