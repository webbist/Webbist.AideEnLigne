using AssistClub.Application.DTOs;
using AssistClub.Application.Interfaces;

namespace AssistClub.Application.Services;

/// <summary>
/// Service responsible for handling user-related business logic.
/// This service ensures that user data is properly retrieved, processed, and transformed 
/// before being sent to the presentation layer. It acts as a bridge between 
/// the repository and API consumers.
/// </summary>
public class UserService(IUserRepository userRepository) : IUserService
{
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