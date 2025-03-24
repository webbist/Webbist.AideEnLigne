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
    Task<User?> GetUserByEmailAsync(string email);
    
    /// <summary>
    /// Creates a new user in the database.
    /// </summary>
    /// <param name="user">The user to create.</param>
    /// <returns>
    /// The newly created <see cref="User"/> entity.
    /// </returns>
    Task<User> CreateUserAsync(User user);
}