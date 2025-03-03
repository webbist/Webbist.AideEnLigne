using AssistClub.Application.DTOs;
using AssistClub.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AssistClub.API.Controllers;

/// <summary>
/// API Controller for managing user-submitted questions.
/// </summary>
/// <param name="questionService">The question service.</param>
/// <param name="logger">The logger.</param>
[ApiController]
[Route("v1/[controller]")]
public class QuestionController(IQuestionService questionService, ILogger<QuestionController> logger) : ControllerBase
{
    /// <summary>
    /// Creates a new question in the system.
    /// Ensures the question follows platform policies before saving it to the database.
    /// </summary>
    /// <param name="questionDto">The question data submitted by the user.</param>
    /// <returns>
    /// - 200 OK: Returns the created question details.
    /// - 500 Internal Server Error: If an unexpected error occurs.
    /// </returns>
    [Route("Create")]
    [HttpPost]
    public async Task<IActionResult> CreateQuestion(QuestionRequestDto questionDto)
    {
        try
        {
            var question = await questionService.CreateQuestionAsync(questionDto);
            logger.LogInformation("Question created with title {title}", question.Title);
            return Ok(question);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while creating the question.");
            return StatusCode(500, e.Message);
        }
    }
}