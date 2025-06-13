using Microsoft.AspNetCore.Mvc;
using Webbist.AideEnLigne.Services.AnswerVotes;

namespace AssistClub.API.Controllers;

/// <summary>
/// Represents the controller responsible for managing answer vote-related API endpoints.
/// </summary>
/// <param name="voteService">The service responsible for answer vote management.</param>
/// <param name="logger">The logging service.</param>
[ApiController]
[Route("v1/[controller]")]
public class AnswerVoteController(IAnswerVoteService voteService, ILogger<AnswerVoteController> logger) : ControllerBase
{
    /// <summary>
    /// Toggles the vote for a specific answer by a user.
    /// </summary>
    /// <param name="request">The request containing the user ID and answer ID.</param>
    /// <returns>
    /// - <c>200 OK</c>: If the vote was successfully toggled. <br/>
    /// - <c>404 Not Found</c>: If the answer or user does not exist. <br/>
    /// - <c>500 Internal Server Error</c>: If an unexpected error occurs.
    /// </returns>
    [HttpPost("toggle")]
    public async Task<IActionResult> ToggleVote([FromBody] AnswerVoteRequest request)
    {
        try
        {
            var result = await voteService.ToggleVoteAsync(request);
            return result ? Ok() : NotFound();
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while toggling the vote.");
            return StatusCode(500, e.Message);
        }
    }
}