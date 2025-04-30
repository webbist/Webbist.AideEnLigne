using AssistClub.Application.DTOs;

namespace AssistClub.Application.Interfaces;

/// <summary>
/// Represents the service interface for handling user-related business logic.
/// </summary>
/// <remarks>
/// Ensures that user data is processed correctly before being sent to the presentation layer, 
/// maintaining security and consistency.
/// </remarks>
public interface IUserService
{
    /// <summary>
    /// Retrieves a user's profile information using their email address.
    /// </summary>
    /// <param name="email">The email of the user to retrieve.</param>
    /// <returns>
    /// A <see cref="UserResponseDto"/> containing user details if found; otherwise, <c>null</c>.
    /// </returns>
    Task<UserResponseDto?> GetUserByEmailAsync(string email);
    
    /// <summary>
    /// Creates a new user in the system.
    /// </summary>
    /// <param name="user">The user details to create.</param>
    /// <returns>A <see cref="UserResponseDto"/> containing the created user details.</returns>
    Task<UserResponseDto> CreateUserAsync(UserRequest user);
    
    /// <summary>
    /// Retrieves a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>
    /// A <see cref="UserResponseDto"/> containing user details if found; otherwise, <c>null</c>.
    /// </returns>
    Task<UserResponseDto?> GetUserByIdAsync(Guid id);

    /// <summary>
    /// Retrieves the notification preferences for a user by their ID.
    /// </summary>
    /// <param name="userId">The ID of the user whose notification preferences to retrieve.</param>
    /// <returns>
    /// A <see cref="NotificationPreferenceRequest"/> containing the user's notification preferences if found; otherwise, <c>null</c>.
    /// </returns>
    Task<NotificationPreferenceRequest?> GetUserNotificationPreferencesAsync(Guid userId);
    
    /// <summary>
    /// Updates the notification preferences for a user.
    /// </summary>
    /// <param name="preferences">The <see cref="NotificationPreferenceRequest"/> containing the updated preferences.</param>
    /// <returns>
    /// Returns <c>true</c> if the update was successful; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> UpdateUserNotificationPreferencesAsync(NotificationPreferenceRequest preferences);
}