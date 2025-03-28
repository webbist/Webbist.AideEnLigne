using AssistClub.Application.DTOs;
using AssistClub.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace AssistClub.API.Controllers;

/// <summary>
/// Represents the controller responsible for managing answer-related API endpoints.
/// </summary>
/// <param name="answerService">The service responsible for answer management.</param>
/// <param name="logger">The logging service.</param>
[ApiController]
[Route("v1/[controller]")]
public class AnswerController(IAnswerService answerService, ILogger<AnswerController> logger) : ControllerBase
{
    /// <summary>
    /// Creates a new answer in the system.
    /// </summary>
    /// <remarks>
    /// This endpoint is used by the frontend application to submit user-generated answers 
    /// to the system, ensuring the data is validated and stored appropriately.
    /// </remarks>
    /// <param name="answerRequest">The answer data submitted by the user.</param>
    /// <returns>
    /// - <c>200 OK</c>: Returns the created answer details. <br/>
    /// - <c>500 Internal Server Error</c>: If an unexpected error occurs.
    /// </returns>
    [Route("Create")]
    [HttpPost]
    public async Task<IActionResult> CreateAnswer(AnswerRequest answerRequest)
    {
        try
        {
            var answer = await answerService.CreateAnswerAsync(answerRequest);
            logger.LogTrace("Answer created with content {content}", answer.Content);
            return Ok(answer);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while creating the answer.");
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// Retrieves all answers in the system with OData support.
    /// </summary>
    /// <remarks>
    /// Supported OData parameters include:
    /// <para><c>$orderby</c>: Enables sorting answers (e.g., <c>$orderby=CreatedAt asc</c>).</para>
    /// <para><c>$select</c>: Retrieves only specific fields (e.g., <c>$select=Content,CreatedAt</c>).</para>
    /// <para><c>$expand</c>: Includes related entities, such as user details (e.g., <c>$expand=User</c>).</para>
    /// </remarks>
    /// <returns>
    /// <c>200 OK</c>: Returns a list of answers.<br/>
    /// <c>500 Internal Server Error</c>: If an unexpected error occurs.
    /// </returns>
    [Route("All")]
    [HttpGet]
    [EnableQuery]
    public async Task<IActionResult> GetAnswers()
    {
        try
        {
            var answers = await answerService.GetAnswersAsync();
            return Ok(answers);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while retrieving the answers.");
            return StatusCode(500, e.Message);
        }
    }
}