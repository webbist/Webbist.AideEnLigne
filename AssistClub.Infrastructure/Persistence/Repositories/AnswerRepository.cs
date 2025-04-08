using AssistClub.Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AssistClub.Infrastructure.Persistence.Repositories;

/// <inheritdoc/>
public class AnswerRepository(AssistClubDbContext db, ILogger<AnswerRepository> logger) : IAnswerRepository
{
    /// <summary>
    /// Adds a new answer to the database and updates the status of the associated question.
    /// </summary>
    /// <param name="answer">The <see cref="Answer"/> entity to add.</param>
    /// <returns>The <see cref="Answer"/> entity that was added.</returns>
    /// <exception cref="DbUpdateException">Thrown if an error occurs while saving the answer to the database.</exception>
    public async Task<Answer> CreateAnswerAsync(Answer answer)
    {
        try
        {
            var question = await db.Questions.FindAsync(answer.QuestionId);
            if (question != null)
            {
                question.Status = QuestionStatus.Pending.ToString();
                db.Questions.Update(question);
            }
            var result = db.Answers.Add(answer);
            await db.SaveChangesAsync();
            return result.Entity;
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "An error occurred while adding a new answer to the database.");
            throw;
        }
    }

    /// <summary>
    /// Retrieves all answers from the database, allowing further filtering, sorting, and pagination.
    /// </summary>
    /// <returns>
    /// An <see cref="IQueryable{T}"/> representing the answers in the database.
    /// </returns>
    public async Task<IQueryable<Answer>> GetAnswers()
    {
        return await Task.FromResult(db.Answers);
    }
    
    /// <summary>
    /// Updates the status of an answer and the associated question status.
    /// </summary>
    /// <param name="answerId">The unique identifier of the answer to be updated.</param>
    /// <param name="newStatus">The new status to be set for the answer.</param>
    /// <returns>
    /// Returns <c>true</c> if the update was successful; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="DbUpdateException">Thrown if an error occurs while saving the answer to the database.</exception>
    public async Task<bool> UpdateAnswerStatusAsync(Guid answerId, AnswerStatus newStatus)
    {
        try
        {
            var answer = await db.Answers.FindAsync(answerId);
            if (answer == null) return false;
            answer.Status = newStatus.ToString();
            db.Answers.Update(answer);
            var question = await db.Questions.FindAsync(answer.QuestionId);
            if (question != null)
            {
                question.Status = newStatus == AnswerStatus.Official
                    ? QuestionStatus.Resolved.ToString()
                    : QuestionStatus.Pending.ToString();
                db.Questions.Update(question);
            }
            return await db.SaveChangesAsync() > 0;
        }
        catch (DbUpdateException e)
        {
            logger.LogError(e, "An error occurred while updating the official status of the answer.");
            throw;
        }
    }

    /// <summary>
    /// Updates an existing answer in the database.
    /// </summary>
    /// <param name="updatedAnswer">The <see cref="Answer"/> entity to update.</param>
    /// <returns>
    /// Returns <c>true</c> if the update was successful; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="DbUpdateException">Thrown if an error occurs while saving the answer to the database.</exception>
    public async Task<bool> UpdateAnswerAsync(Answer updatedAnswer)
    {
        try
        {
            var answer = await db.Answers.FindAsync(updatedAnswer.Id);
            if (answer == null) return false;
            answer.Content = updatedAnswer.Content;
            answer.UpdatedAt = updatedAnswer.UpdatedAt;
            db.Answers.Update(answer);
            return await db.SaveChangesAsync() > 0;
        }
        catch (DbUpdateException e)
        {
            logger.LogError(e, "An error occurred while updating the answer.");
            throw;
        }
    }
}