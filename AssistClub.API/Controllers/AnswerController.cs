using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Webbist.AideEnLigne.Data;
using Webbist.AideEnLigne.Services.Answers;

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
    /// <param name="userId">The ID of the user to check if he voted for any answer.</param>
    /// <returns>
    /// <c>200 OK</c>: Returns a list of answers.<br/>
    /// <c>500 Internal Server Error</c>: If an unexpected error occurs.
    /// </returns>
    [Route("All/{userId}")]
    [HttpGet]
    [EnableQuery]
    public async Task<IActionResult> GetAnswers(Guid userId)
    {
        try
        {
            var answers = await answerService.GetAnswersAsync(userId);
            return Ok(answers);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while retrieving the answers.");
            return StatusCode(500, e.Message);
        }
    }
    
    /// <summary>
    /// Updates the status of an answer.
    /// </summary>
    /// <param name="id">The ID of the answer to be updated.</param>
    /// <param name="newStatus">The new status to be set for the answer.</param>
    /// <returns>
    /// - <c>200 OK</c>: If the answer status was successfully updated. <br/>
    /// - <c>404 Not Found</c>: If the answer with the specified ID does not exist. <br/>
    /// - <c>500 Internal Server Error</c>: If an unexpected error occurs.
    /// </returns>
    [HttpPut("Status/{id}")]
    public async Task<IActionResult> UpdateAnswerStatus(Guid id, [FromBody] AnswerStatus newStatus)
    {
        try
        {
            var result = await answerService.UpdateAnswerStatusAsync(id, newStatus);
            return result ? Ok() : NotFound();
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while updating the answer status.");
            return StatusCode(500, e.Message);
        }
    }
    
    /// <summary>
    /// Updates an existing answer in the system.
    /// </summary>
    /// <param name="id">The ID of the answer to be updated.</param>
    /// <param name="answerRequest">The updated answer data.</param>
    /// <returns>
    /// - <c>200 OK</c>: If the answer was successfully updated.<br/>
    /// - <c>404 Not Found</c>: If the answer with the specified ID does not exist. <br/>
    /// - <c>500 Internal Server Error</c>: If an unexpected error occurs.
    /// </returns>
    [HttpPut("Update/{id}")]
    public async Task<IActionResult> UpdateAnswer(Guid id, [FromBody] AnswerRequest answerRequest)
    {
        try
        {
            var result = await answerService.UpdateAnswerAsync(id, answerRequest);
            return result ? Ok() : NotFound();
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while updating the answer.");
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// Deletes an answer from the system.
    /// </summary>
    /// <param name="id">The ID of the answer to be deleted.</param>
    /// <returns>
    /// - <c>200 OK</c>: If the answer was successfully deleted. <br/>
    /// - <c>500 Internal Server Error</c>: If an unexpected error occurs.
    /// </returns>
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteAnswer(Guid id)
    {
        try
        {
            var result = await answerService.DeleteAnswerAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while deleting the answer.");
            return StatusCode(500, e.Message);
        }
    }
}