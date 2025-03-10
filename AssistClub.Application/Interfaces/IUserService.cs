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
}