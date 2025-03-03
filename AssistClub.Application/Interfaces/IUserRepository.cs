using Domain.Entities;

namespace AssistClub.Application.Interfaces;

/// <summary>
/// Repository interface for managing user-related data operations.
/// This repository abstracts the database access layer, ensuring that 
/// business logic does not directly interact with the database.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Retrieves a user by their email address.
    /// </summary>
    /// <param name="email">The email of the user.</param>
    /// <returns>
    /// A <see cref="User"/> entity if found; otherwise, null.
    /// </returns>
    Task<User?> GetUserByEmailAsync(string email);
}