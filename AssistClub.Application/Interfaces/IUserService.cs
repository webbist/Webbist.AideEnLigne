using AssistClub.Application.DTOs;

namespace AssistClub.Application.Interfaces;

/// <summary>
/// Service interface for handling user-related business logic.
/// This service ensures that user data is processed correctly before being 
/// sent to the presentation layer, maintaining security and consistency.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Retrieves a user's profile information using their email address.
    /// </summary>
    /// <param name="email">The email of the user to retrieve.</param>
    /// <returns>
    /// A <see cref="UserResponseDto"/> containing user details if found; otherwise, null.
    /// </returns>
    Task<UserResponseDto?> GetUserByEmailAsync(string email);
}