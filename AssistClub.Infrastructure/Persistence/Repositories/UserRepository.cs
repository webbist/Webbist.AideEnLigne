using AssistClub.Application.Interfaces;
using Domain.Entities;
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
                .SingleOrDefaultAsync();
        }
        catch (InvalidOperationException ex)
        {
            logger.LogError(ex, "Multiple users found with the same ID: {Id}", id);
            throw new InvalidOperationException("Data integrity issue: multiple users found with the same ID.", ex);
        }
    }
}