using AssistClub.Application.DTOs;
using AssistClub.Application.Interfaces;
using Domain.Entities;

namespace AssistClub.Application.Services;

/// <inheritdoc/>
public class UserService(IUserRepository userRepository) : IUserService
{
    /// <summary>
    /// Retrieves a user's profile information using their email address.
    /// </summary>
    /// <param name="email">The email of the user to retrieve.</param>
    /// <returns>
    /// A <see cref="UserResponseDto"/> containing user details if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<UserResponseDto?> GetUserByEmailAsync(string email)
    {
        var user = await userRepository.GetUserByEmailAsync(email);
        if (user != null)
            return new UserResponseDto
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Photo = user.Photo,
                Club = user.Club,
                Microsite = user.Microsite,
                Role = user.Role
            };
        return null;
    }

    /// <summary>
    /// Creates a new user in the system.
    /// </summary>
    /// <param name="user">The user details to create.</param>
    /// <returns>A <see cref="UserResponseDto"/> containing the created user details.</returns>
    public async Task<UserResponseDto> CreateUserAsync(UserRequest user)
    {
        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Firstname = user.Firstname,
            Lastname = user.Lastname,
            Email = user.Email,
            Photo = user.Photo,
            Club = user.Club,
            Microsite = user.Microsite,
            Role = user.Role.ToString().ToLower()
        };
        
        var createdUser = await userRepository.CreateUserAsync(newUser);

        return new UserResponseDto
        {
            Id = createdUser.Id,
            Firstname = createdUser.Firstname,
            Lastname = createdUser.Lastname,
            Email = createdUser.Email,
            Photo = createdUser.Photo,
            Club = createdUser.Club,
            Microsite = createdUser.Microsite,
            Role = createdUser.Role
        };
    }
    
    /// <summary>
    /// Retrieves a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>
    /// A <see cref="UserResponseDto"/> containing user details if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<UserResponseDto?> GetUserByIdAsync(Guid id)
    {
        var user = await userRepository.GetUserByIdAsync(id);
        if (user != null)
            return new UserResponseDto
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Photo = user.Photo,
                Club = user.Club,
                Microsite = user.Microsite,
                Role = user.Role
            };
        return null;
    }
    
    /// <summary>
    /// Retrieves the notification preferences for a user by their ID.
    /// </summary>
    /// <param name="userId">The ID of the user whose notification preferences to retrieve.</param>
    /// <returns>
    /// A <see cref="NotificationPreferenceRequest"/> containing the user's notification preferences if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<NotificationPreferenceRequest?> GetUserNotificationPreferencesAsync(Guid userId)
    {
        var preferences = await userRepository.GetUserNotificationPreferencesAsync(userId);
        if (preferences != null)
            return new NotificationPreferenceRequest
            {
                UserId = preferences.UserId,
                NotifyOnNewClubQuestion = preferences.NotifyOnNewClubQuestion.Value,
                NotifyOnAnswerPublishedOnMyQuestion = preferences.NotifyOnAnswerPublishedOnMyQuestion.Value,
                NotifyOnAnswerToMyQuestionMarkedOfficial = preferences.NotifyOnAnswerToMyQuestionMarkedOfficial.Value,
                NotifyOnMyQuestionOrAnswerModifiedByAdmin = preferences.NotifyOnMyQuestionOrAnswerModifiedByAdmin.Value,
                NotifyOnAnyOfficialAnswerInQuestionIrelated = preferences.NotifyOnAnyOfficialAnswerInQuestionIrelated.Value,
                NotifyOnQuestionIrelatedModifiedByAuthor = preferences.NotifyOnQuestionIrelatedModifiedByAuthor.Value,
                NotifyOnNewAnswerInQuestionIrelated = preferences.NotifyOnNewAnswerInQuestionIrelated.Value
            };
        return null;
    }

    /// <summary>
    /// Updates the notification preferences for a user.
    /// </summary>
    /// <param name="preferences">The <see cref="NotificationPreferenceRequest"/> containing the updated preferences.</param>
    /// <returns>
    /// Returns <c>true</c> if the update was successful; otherwise, <c>false</c>.
    /// </returns>
    public async Task<bool> UpdateUserNotificationPreferencesAsync(NotificationPreferenceRequest preferences)
    {
        var updatedPreferences = new NotificationPreference
        {
            UserId = preferences.UserId,
            NotifyOnNewClubQuestion = preferences.NotifyOnNewClubQuestion,
            NotifyOnAnswerPublishedOnMyQuestion = preferences.NotifyOnAnswerPublishedOnMyQuestion,
            NotifyOnAnswerToMyQuestionMarkedOfficial = preferences.NotifyOnAnswerToMyQuestionMarkedOfficial,
            NotifyOnMyQuestionOrAnswerModifiedByAdmin = preferences.NotifyOnMyQuestionOrAnswerModifiedByAdmin,
            NotifyOnAnyOfficialAnswerInQuestionIrelated = preferences.NotifyOnAnyOfficialAnswerInQuestionIrelated,
            NotifyOnQuestionIrelatedModifiedByAuthor = preferences.NotifyOnQuestionIrelatedModifiedByAuthor,
            NotifyOnNewAnswerInQuestionIrelated = preferences.NotifyOnNewAnswerInQuestionIrelated
        };
        
        return await userRepository.UpdateNotificationPreferencesAsync(updatedPreferences);
    }
}