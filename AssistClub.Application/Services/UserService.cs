using AssistClub.Application.DTOs;
using AssistClub.Application.Interfaces;

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
                Microsite = user.Microsite
            };
        return null;
    }
}