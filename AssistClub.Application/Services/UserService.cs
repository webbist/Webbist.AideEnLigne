using AssistClub.Application.DTOs;
using AssistClub.Application.Interfaces;
using Domain.Entities;

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
                Microsite = user.Microsite,
                Role = user.Role
            };
        return null;
    }

    /// <summary>
    /// Creates a new user in the system.
    /// </summary>
    /// <param name="user">The user details to create.</param>
    /// <returns>A <see cref="UserResponseDto"/> containing the created user details.</returns>
    public async Task<UserResponseDto> CreateUserAsync(UserRequest user)
    {
        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Firstname = user.Firstname,
            Lastname = user.Lastname,
            Email = user.Email,
            Photo = user.Photo,
            Club = user.Club,
            Microsite = user.Microsite,
            Role = user.Role.ToString().ToLower()
        };
        
        var createdUser = await userRepository.CreateUserAsync(newUser);

        return new UserResponseDto
        {
            Id = createdUser.Id,
            Firstname = createdUser.Firstname,
            Lastname = createdUser.Lastname,
            Email = createdUser.Email,
            Photo = createdUser.Photo,
            Club = createdUser.Club,
            Microsite = createdUser.Microsite,
            Role = createdUser.Role
        };
    }
}