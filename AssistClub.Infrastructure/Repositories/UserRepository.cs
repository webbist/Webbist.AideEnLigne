// Copyright (C) 2025 Webbist
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, see <https://www.gnu.org/licenses/old-licenses/gpl-2.0.html>.

using AssistClub.Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Webbist.AideEnLigne.Data.Repositories
{
    /// <inheritdoc/>
    public class UserRepository(DataContext db, ILogger<UserRepository> logger) : IUserRepository
    {
        #region Methods
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
                        NotifyOnAnyOfficialAnswerInQuestionIrelated = true,
                        NotifyOnQuestionIrelatedModifiedByAuthor = true,
                        NotifyOnNewAnswerInQuestionIrelated = true
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
        /// This method gets all emails of users who are not the author of the question and
        /// are either admins or belong to the same club as the question and have opted in to receive notifications.
        /// </remarks>
        /// <param name="authorId">The ID of the author of the question.</param>
        /// <param name="club">The club associated with the question.</param>
        /// <returns>
        /// A collection of <c>string</c> representing the email addresses of the users to notify.
        /// </returns>
        public async Task<IEnumerable<string>> GetEmailsToNotifyOnNewQuestion(Guid authorId, string club)
        {
            return await Task.FromResult(db.Users
                .Where(u =>
                    u.Id != authorId &&
                    (u.Role == Role.Admin.ToString() || u.Club == club) &&
                    (u.NotificationPreference.NotifyOnNewClubQuestion || u.Role == Role.Admin.ToString())
                )
                .Select(u => u.Email)
                .Distinct());
        }

        /// <summary>
        /// Retrieves the users to notify when a question is updated.
        /// </summary>
        /// <remarks>
        /// This method gets all emails of users who have answered the question
        /// and have opted in to receive notifications.
        /// </remarks>
        /// <param name="questionId">The ID of the question.</param>
        /// <param name="authorId">The ID of the author of the question.</param>
        /// <returns>
        /// A collection of <c>string</c> representing the email addresses of the users to notify.
        /// </returns>
        public async Task<IEnumerable<string>> GetEmailsToNotifyOnUpdateQuestion(Guid questionId, Guid authorId)
        {
            return await Task.FromResult(db.Answers
                .Where(a => a.QuestionId == questionId &&
                            a.UserId != authorId &&
                            a.User.NotificationPreference.NotifyOnQuestionIrelatedModifiedByAuthor == true)
                .Select(a => a.User.Email)
                .Distinct());
        }

        /// <summary>
        /// Retrieves the users to notify when an official answer is updated.
        /// </summary>
        /// <remarks>
        /// This method gets all emails of users who have answered the question, admins and the author of the question.
        /// </remarks>
        /// <param name="questionId">The ID of the question.</param>
        /// <returns>
        /// A collection of <c>string</c> representing the email addresses of the users to notify.
        /// </returns>
        public async Task<IEnumerable<string>> GetEmailsToNotifyOnUpdateOfficialAnswer(Guid questionId)
        {
            return await Task.FromResult(db.Users
                .Where(u =>
                    u.Role == Role.Admin.ToString() ||
                    db.Questions.Any(q => q.Id == questionId && q.UserId == u.Id) ||
                    db.Answers.Any(a => a.QuestionId == questionId && a.UserId == u.Id)
                )
                .Select(u => u.Email)
                .Distinct());
        }

        /// <summary>
        /// Retrieves the users to notify when a new answer is posted.
        /// </summary>
        /// <remarks>
        /// This method gets all emails of users who are not the author of the answer or question but
        /// have answered the question or are admins and have opted in to receive notifications.
        /// </remarks>
        /// <param name="answerAuthorId">The ID of the author of the answer.</param>
        /// <param name="question">The question associated with the answer.</param>
        /// <returns>
        /// A collection of <c>string</c> representing the email addresses of the users to notify.
        /// </returns>
        public async Task<IEnumerable<string>> GetEmailsToNotifyOnNewAnswer(Guid answerAuthorId, Question question)
        {
            return await Task.FromResult(db.Users
                .Where(u =>
                    u.Id != answerAuthorId &&
                    (u.Role == Role.Admin.ToString() ||
                     db.Answers.Any(a => a.QuestionId == question.Id && a.UserId == u.Id && a.UserId != question.UserId))
                     && u.NotificationPreference.NotifyOnNewAnswerInQuestionIrelated == true
                )
                .Select(u => u.Email)
                .Distinct());
        }

        /// <summary>
        /// Retrieves the users to notify when an answer is marked as official.
        /// </summary>
        /// <remarks>
        /// This method gets all users who are either the author of the answer,
        /// the author of the question, or have answered the question.
        /// </remarks>
        /// <param name="answerAuthorId">The ID of the author of the answer.</param>
        /// <param name="question">The question associated with the answer.</param>
        /// <returns>
        /// A collection of <c>string</c> representing the email addresses of the users to notify.
        /// </returns>
        public async Task<IEnumerable<string>> GetEmailsToNotifyOnOfficialAnswer(Guid answerAuthorId, Question question)
        {
            return await Task.FromResult(db.Users
                 .Where(u =>
                     u.Id != question.UserId &&
                     u.NotificationPreference.NotifyOnAnyOfficialAnswerInQuestionIrelated == true &&
                     (u.Id == answerAuthorId ||
                     db.Answers.Any(a => a.QuestionId == question.Id && a.UserId == u.Id))
                 )
                 .Select(u => u.Email)
                 .Distinct());
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
        #endregion
    }
}