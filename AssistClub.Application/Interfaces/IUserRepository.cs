using AssistClub.Application.DTOs;
using Domain.Entities;

namespace AssistClub.Application.Interfaces;

/// <summary>
/// Interface for the user repository.
/// </summary>
public interface IUserRepository
{
    
    /// <summary>
    /// Gets a user by their email.
    /// </summary>
    /// <param name="email">The email of the user.</param>
    /// <returns>A UserResponseDto if the user is found, otherwise null.</returns>
    UserResponseDto? GetUserByEmail(string email);
}