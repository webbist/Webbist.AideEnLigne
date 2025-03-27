using AssistClub.Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AssistClub.Infrastructure.Persistence.Repositories;

/// <inheritdoc/>
public class AnswerRepository(AssistClubDbContext db, ILogger<AnswerRepository> logger) : IAnswerRepository
{
    /// <summary>
    /// Adds a new answer to the database.
    /// </summary>
    /// <param name="answer">The <see cref="Answer"/> entity to add.</param>
    /// <returns>The <see cref="Answer"/> entity that was added.</returns>
    /// <exception cref="DbUpdateException">Thrown if an error occurs while saving the answer to the database.</exception>
    public async Task<Answer> CreateAnswerAsync(Answer answer)
    {
        try
        {
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
}