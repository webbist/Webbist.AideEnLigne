using AssistClub.Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssistClub.Infrastructure.Persistence.Repositories;

public class QuestionRepository(AssistClubDbContext db) : IQuestionRepository
{
    public async Task<Question> CreateQuestionAsync(Question question)
    {
        var result = db.Questions.Add(question);
        await db.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Question?> GetQuestionByIdAsync(Guid id)
    {
        return await db.Questions.FindAsync(id);
    }

    public async Task<List<Question>> GetAllQuestionsAsync(string? visibility = null)
    {
        return visibility is null
            ? await db.Questions.ToListAsync()
            : await db.Questions.Where(q => q.Visibility == visibility).ToListAsync();
    }

    public async Task<bool> UpdateQuestionAsync(Question question)
    {
        db.Questions.Update(question);
        return await db.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteQuestionAsync(Guid id)
    {
        var question = await db.Questions.FindAsync(id);
        if (question is null) return false;
        db.Questions.Remove(question);
        return await db.SaveChangesAsync() > 0;
    }
}