using Microsoft.AspNetCore.Mvc;
using Webbist.AideEnLigne.Services.Users;

namespace AssistClub.API.Controllers;

/// <summary>
/// Represents the API controller for user-related operations.
/// </summary>
/// <param name="userService">The service responsible for user management.</param>
/// <param name="logger">The logger.</param>
[ApiController]
[Route("v1/[controller]")]
public class UserController(IUserService userService, ILogger<UserController> logger) : ControllerBase
{
    /// <summary>
    /// Retrieves user details based on the provided email.
    /// </summary>
    /// <param name="email">The email address of the user to retrieve.</param>
    /// <returns>
    /// - <c>200 OK</c>: Returns the user data if found.<br/>
    /// - <c>404 Not Found</c>: If no user is associated with the given email.<br/>
    /// - <c>500 Internal Server Error</c>: If an unexpected error occurs.
    /// </returns>
    [HttpGet("{email}")]
    public async Task<IActionResult> GetUser(string email)
    {
        try
        {
            var user = await userService.GetUserByEmailAsync(email);
            if (user == null)
            {
                logger.LogWarning("User with email {email} not found.", email);
                return NotFound();
            }
            logger.LogTrace("User found with email {email}", email);
            return Ok(user);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while getting the user.");
            return StatusCode(500, e.Message);
        }
    }
    
    /// <summary>
    /// Creates a new user in the system.
    /// </summary>
    /// <remarks>
    /// Ensures the user follows platform policies before saving it to the database.
    /// </remarks>
    /// <param name="user">The user data submitted by the user.</param>
    /// <returns>
    /// - <c>200 OK</c>: Returns the created user details.<br/>
    /// - <c>500 Internal Server Error</c>: If an unexpected error occurs.
    /// </returns>
    [Route("Create")]
    [HttpPost]
    public async Task<IActionResult> CreateUser(UserRequest user)
    {
        try
        {
            var newUser = await userService.CreateUserAsync(user);
            logger.LogTrace("User created with email {email}", newUser.Email);
            return Ok(newUser);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while creating the user.");
            return StatusCode(500, e.Message);
        }
    }
    
    /// <summary>
    /// Retrieves user details based on the provided ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>
    /// - <c>200 OK</c>: Returns the user data if found.<br/>
    /// - <c>404 Not Found</c>: If no user is associated with the given ID.<br/>
    /// - <c>500 Internal Server Error</c>: If an unexpected error occurs.
    /// </returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        try
        {
            var user = await userService.GetUserByIdAsync(id);
            if (user == null)
            {
                logger.LogWarning("User with ID {id} not found.", id);
                return NotFound();
            }
            logger.LogTrace("User found with ID {id}", id);
            return Ok(user);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while getting the user.");
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("notifications/{id:guid}")]
    public async Task<IActionResult> GetUserNotificationPreferences(Guid id)
    {
        try
        {
            var preferences = await userService.GetUserNotificationPreferencesAsync(id);
            if (preferences == null)
            {
                logger.LogWarning("Notification preferences for user with ID {id} not found.", id);
                return NotFound();
            }
            logger.LogTrace("Notification preferences found for user with ID {id}", id);
            return Ok(preferences);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while getting the user's notification preferences.");
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPut("notifications")]
    public async Task<IActionResult> UpdateUserNotificationPreferences(NotificationPreferenceRequest preferences)
    {
        try
        {
            var result = await userService.UpdateUserNotificationPreferencesAsync(preferences);
            return result ? Ok() : NotFound();
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while updating the user's notification preferences.");
            return StatusCode(500, e.Message);
        }
    }
}