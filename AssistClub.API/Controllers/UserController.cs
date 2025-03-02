using AssistClub.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AssistClub.API.Controllers;

/// <summary>
/// API controller for user-related operations.
/// </summary>
[ApiController]
[Route("v1/[controller]")]
public class UserController(IUserRepository userRepository, ILogger<UserController> logger) : ControllerBase
{
    /// <summary>
    /// Gets a user by their email.
    /// </summary>
    /// <param name="email">The email of the user.</param>
    /// <returns>An ActionResult containing the user data or an error status.</returns>
    [HttpGet("{email}")]
    public ActionResult GetUser(string email)
    {
        try
        {
            var user = userRepository.GetUserByEmail(email);
            if (user == null)
            {
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