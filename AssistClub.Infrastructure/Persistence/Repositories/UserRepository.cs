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
}