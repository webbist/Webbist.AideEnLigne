using Domain.Entities;

namespace AssistClub.Application.Interfaces;

/// <summary>
/// Represents the repository interface for managing user-related data operations.
/// </summary>
/// <remarks>
/// Abstracts the database access layer to maintain separation of concerns 
/// and ensure that business logic does not directly interact with the database.
/// </remarks>
public interface IUserRepository
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
    Task<User?> GetUserByEmailAsync(string email);
    
    /// <summary>
    /// Creates a new user in the database.
    /// </summary>
    /// <param name="user">The user to create.</param>
    /// <returns>
    /// The newly created <see cref="User"/> entity.
    /// </returns>
    Task<User> CreateUserAsync(User user);

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
    Task<User?> GetUserByIdAsync(Guid id);

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
    Task<IEnumerable<User>> GetEmailsToNotifyOnNewQuestion(Guid authorId, string club);

    /// <summary>
    /// Retrieves the users to notify when a question is updated.
    /// </summary>
    /// <remarks>
    /// This method gets all users who have answered the question and are either admins
    /// </remarks>
    /// <param name="questionId">The ID of the question.</param>
    /// <returns>
    /// A collection of <see cref="User"/> entities representing the users to notify.
    /// </returns>
    Task<IEnumerable<User>> GetEmailsToNotifyOnUpdateQuestion(Guid questionId);

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
    Task<IEnumerable<User>> GetEmailsToNotifyOnUpdateOfficialAnswer(Guid questionId);

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
    Task<IEnumerable<User>> GetEmailsToNotifyOnNewAnswer(Guid answerAuthorId, Guid questionAuthorId, Guid questionId);

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
    Task<IEnumerable<User>> GetEmailsToNotifyOnOfficialAnswer(Guid answerAuthorId, Guid questionId);
    
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
    Task<NotificationPreference?> GetUserNotificationPreferencesAsync(Guid userId);

    /// <summary>
    /// Updates the notification preferences for a user.
    /// </summary>
    /// <param name="updatedPreferences">The updated notification preferences.</param>
    /// <returns>
    /// A <c>bool</c> indicating whether the update was successful or not.
    /// </returns>
    Task<bool> UpdateNotificationPreferencesAsync(NotificationPreference updatedPreferences);
}