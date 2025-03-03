using AssistClub.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AssistClub.API.Controllers;

/// <summary>
/// API controller responsible for user-related operations.
/// </summary>
/// <param name="userService">The user service.</param>
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
    /// - 200 OK: Returns the user data if found.
    /// - 404 Not Found: If no user is associated with the given email.
    /// - 500 Internal Server Error: If an unexpected error occurs.
    /// </returns>
    [HttpGet("{email}")]
    public async Task<IActionResult> GetUser(string email)
    {
        try
        {
            var user = await userService.GetUserByEmailAsync(email);
            if (user == null)
            {
                logger.LogError("User with email {email} not found.", email);
                return NotFound();
            }
            logger.LogInformation("User found with email {email}", email);
            return Ok(user);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while getting the user.");
            return StatusCode(500, e.Message);
        }
    }
}