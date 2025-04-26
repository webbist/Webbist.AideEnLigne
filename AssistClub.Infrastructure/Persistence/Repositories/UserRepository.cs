using AssistClub.Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AssistClub.Infrastructure.Persistence.Repositories;

/// <inheritdoc/>
public class UserRepository(AssistClubDbContext db, ILogger<UserRepository> logger): IUserRepository
{
    /// <summary>
    /// Retrieves a user by their email address.
    /// </summary>
    /// <param name="email">The email of the user.</param>
    /// <returns>
    /// A <see cref="User"/> entity if found; otherwise, <c>null</c>.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if multiple users are found with the same email address.
    /// </exception>
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        string normalizedEmail = email.Trim().ToLower();
        try
        {
            return await db.Users
                .Where(u => u.Email.ToLower() == normalizedEmail)
                .SingleOrDefaultAsync();
        }
        catch (InvalidOperationException ex)
        {
            logger.LogError(ex, "Multiple users found with the same email: {Email}", email);
            throw new InvalidOperationException("Data integrity issue: multiple users found with the same email.", ex);
        }
    }

    /// <summary>
    /// Creates a new user in the database.
    /// </summary>
    /// <param name="user">The user to create.</param>
    /// <returns>
    /// The newly created <see cref="User"/> entity.
    /// </returns>
    /// <exception cref="DbUpdateException">
    /// Thrown if an error occurs while saving the user to the database.
    /// </exception>
    public async Task<User> CreateUserAsync(User user)
    {
        try
        {
            var result = db.Users.Add(user);
            await db.SaveChangesAsync();
            if (result.Entity == null)
            {
                throw new DbUpdateException("Failed to save the user to the database.");
            }
            db.NotificationPreferences.Add(
                new NotificationPreference
                {
                    UserId = result.Entity.Id,
                    NotifyOnNewClubQuestion = true,
                    NotifyOnAnswerPublishedOnMyQuestion = true,
                    NotifyOnAnswerToMyQuestionMarkedOfficial = true,
                    NotifyOnMyQuestionOrAnswerModifiedByAdmin = true,
                    NotifyOnAnyOfficialAnswerInQuestionIrelated = true
                }
            );
            await db.SaveChangesAsync();
            return result.Entity;
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "An error occurred while adding a new user to the database.");
            throw;
        }
    }
    
    /// <summary>
    /// Retrieves a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <returns>
    /// A <see cref="User"/> entity if found; otherwise, <c>null</c>.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if multiple users are found with the same ID.
    /// </exception>
    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        try
        {
            return await db.Users
                .Where(u => u.Id == id)
                .Include(u => u.NotificationPreference)
                .SingleOrDefaultAsync();
        }
        catch (InvalidOperationException ex)
        {
            logger.LogError(ex, "Multiple users found with the same ID: {Id}", id);
            throw new InvalidOperationException("Data integrity issue: multiple users found with the same ID.", ex);
        }
    }

    /// <summary>
    /// Retrieves the users to notify when a new question is posted.
    /// </summary>
    /// <remarks>
    /// This method gets all users who are not the author of the question and
    /// are either admins or belong to the same club as the question.
    /// </remarks>
    /// <param name="authorId">The ID of the author of the question.</param>
    /// <param name="club">The club associated with the question.</param>
    /// <returns>
    /// A collection of <see cref="User"/> entities representing the users to notify.
    /// </returns>
    public async Task<IEnumerable<User>> GetEmailsToNotifyOnNewQuestion(Guid authorId, string club)
    {
        return await db.Users
            .Where(u =>
                u.Id != authorId &&
                (u.Role == Role.Admin.ToString() || u.Club == club)
            )
            .Include(u => u.NotificationPreference)
            .Distinct()
            .ToListAsync();
    }
    
    /// <summary>
    /// Retrieves the users to notify when a question is updated.
    /// </summary>
    /// <remarks>
    /// This method gets all users who have answered the question and are admins
    /// </remarks>
    /// <param name="questionId">The ID of the question.</param>
    /// <returns>
    /// A collection of <see cref="User"/> entities representing the users to notify.
    /// </returns>
    public async Task<IEnumerable<User>> GetEmailsToNotifyOnUpdateQuestion(Guid questionId)
    {
        return await db.Answers
            .Where(a => a.QuestionId == questionId)
            .Include(a => a.User)
            .Include(a => a.User.NotificationPreference)
            .Select(a => a.User)
            .Distinct()
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves the users to notify when an official answer is updated.
    /// </summary>
    /// <remarks>
    /// This method gets all users who have answered the question, admins and the author of the question.
    /// </remarks>
    /// <param name="questionId">The ID of the question.</param>
    /// <returns>
    /// A collection of <see cref="User"/> entities representing the users to notify.
    /// </returns>
    public async Task<IEnumerable<User>> GetEmailsToNotifyOnUpdateOfficialAnswer(Guid questionId)
    {
        var users = await db.Users
            .Where(u =>
                u.Role == Role.Admin.ToString() || 
                db.Questions.Any(q => q.Id == questionId && q.UserId == u.Id) ||
                db.Answers.Any(a => a.QuestionId == questionId && a.UserId == u.Id)
            )
            .Distinct()
            .ToListAsync();
        
        return users;
    }

    /// <summary>
    /// Retrieves the users to notify when a new answer is posted.
    /// </summary>
    /// <remarks>
    /// This method gets all users who are not the author of the answer and
    /// are either admins or have answered the question.
    /// </remarks>
    /// <param name="answerAuthorId">The ID of the author of the answer.</param>
    /// <param name="questionAuthorId">The ID of the author of the question.</param>
    /// <param name="questionId">The ID of the question.</param>
    /// <returns>
    /// A collection of <see cref="User"/> entities representing the users to notify.
    /// </returns>
    public async Task<IEnumerable<User>> GetEmailsToNotifyOnNewAnswer(Guid answerAuthorId, Guid questionAuthorId, Guid questionId)
    {
        var users = await db.Users
            .Where(u =>
                u.Id != answerAuthorId && 
                (u.Role == Role.Admin.ToString() || 
                 db.Answers.Any(a => a.QuestionId == questionId && a.UserId == u.Id && a.UserId != questionAuthorId))
            )
            .Include(u => u.NotificationPreference)
            .Distinct()
            .ToListAsync();
        
        return users;
    }

    /// <summary>
    /// Retrieves the users to notify when an answer is marked as official.
    /// </summary>
    /// <remarks>
    /// This method gets all users who are either the author of the answer,
    /// the author of the question, or have answered the question.
    /// </remarks>
    /// <param name="answerAuthorId">The ID of the author of the answer.</param>
    /// <param name="questionId">The ID of the question.</param>
    /// <returns>
    /// A collection of <see cref="User"/> entities representing the users to notify.
    /// </returns>
    public async Task<IEnumerable<User>> GetEmailsToNotifyOnOfficialAnswer(Guid answerAuthorId, Guid questionId)
    {
        var users = await db.Users
            .Where(u =>
                u.Id == answerAuthorId ||
                db.Questions.Any(q => q.Id == questionId && q.UserId == u.Id) ||
                db.Answers.Any(a => a.QuestionId == questionId && a.UserId == u.Id)
            )
            .Include(u => u.NotificationPreference)
            .Distinct()
            .ToListAsync();

        return users;
    }

    /// <summary>
    /// Retrieves the notification preferences for a user by their unique identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>
    /// A <see cref="NotificationPreference"/> entity if found; otherwise, <c>null</c>.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if multiple notification preferences are found for the user.
    /// </exception>
    public async Task<NotificationPreference?> GetUserNotificationPreferencesAsync(Guid userId)
    {
        try
        {
            return await db.NotificationPreferences
                .Where(np => np.UserId == userId)
                .SingleOrDefaultAsync();
        }
        catch (InvalidOperationException ex)
        {
            logger.LogError(ex, "Multiple notification preferences found for user ID: {UserId}", userId);
            throw new InvalidOperationException("Data integrity issue: multiple notification preferences found for user ID.", ex);
        }
    }
    
    /// <summary>
    /// Updates the notification preferences for a user.
    /// </summary>
    /// <param name="updatedPreferences">The updated notification preferences.</param>
    /// <returns>
    /// A <c>bool</c> indicating whether the update was successful or not.
    /// </returns>
    public async Task<bool> UpdateNotificationPreferencesAsync(NotificationPreference updatedPreferences)
    {
        try
        {
            var preferences = await db.NotificationPreferences.FindAsync(updatedPreferences.UserId);
            if (preferences == null) return false;
            preferences.NotifyOnNewClubQuestion = updatedPreferences.NotifyOnNewClubQuestion;
            preferences.NotifyOnAnswerPublishedOnMyQuestion = updatedPreferences.NotifyOnAnswerPublishedOnMyQuestion;
            preferences.NotifyOnAnswerToMyQuestionMarkedOfficial = updatedPreferences.NotifyOnAnswerToMyQuestionMarkedOfficial;
            preferences.NotifyOnMyQuestionOrAnswerModifiedByAdmin = updatedPreferences.NotifyOnMyQuestionOrAnswerModifiedByAdmin;
            preferences.NotifyOnAnyOfficialAnswerInQuestionIrelated = updatedPreferences.NotifyOnAnyOfficialAnswerInQuestionIrelated;
            preferences.NotifyOnQuestionIrelatedModifiedByAuthor = updatedPreferences.NotifyOnQuestionIrelatedModifiedByAuthor;
            preferences.NotifyOnNewAnswerInQuestionIrelated = updatedPreferences.NotifyOnNewAnswerInQuestionIrelated;
            db.NotificationPreferences.Update(preferences);
            return await db.SaveChangesAsync() > 0;
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "An error occurred while updating notification preferences.");
            throw;
        }
    }
}