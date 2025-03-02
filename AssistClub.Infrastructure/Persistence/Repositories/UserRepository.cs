using AssistClub.Application.DTOs;
using AssistClub.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace AssistClub.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository for user-related data operations.
/// </summary>
public class UserRepository(AssistClubDbContext db, ILogger<UserRepository> logger): IUserRepository
{
    public UserResponseDto? GetUserByEmail(string email)
    {
        var user = db.Users.FirstOrDefault(u => u.Email == email);
        if (user != null)
            // Map the user entity to UserResponseDto
            return new UserResponseDto
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Photo = user.Photo,
                Club = user.Club,
                Microsite = user.Microsite
            };
        logger.LogError("User with email {email} not found.", email);
        return null;
    }
}