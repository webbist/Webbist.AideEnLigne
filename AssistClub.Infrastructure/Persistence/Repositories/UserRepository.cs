using AssistClub.Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssistClub.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository responsible for handling database operations related to users.
/// </summary>
public class UserRepository(AssistClubDbContext db): IUserRepository
{
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await db.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}