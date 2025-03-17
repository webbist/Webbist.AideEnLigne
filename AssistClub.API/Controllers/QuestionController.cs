using AssistClub.Application.DTOs;
using AssistClub.Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AssistClub.API.Controllers;

/// <summary>
/// Represents the API Controller for managing user-submitted questions.
/// </summary>
/// <param name="questionService">The service responsible for question management.</param>
/// <param name="logger">The logging service.</param>
[ApiController]
[Route("v1/[controller]")]
public class QuestionController(IQuestionService questionService, ILogger<QuestionController> logger) : ControllerBase
{
    /// <summary>
    /// Creates a new question in the system.
    /// </summary>
    /// <remarks>
    /// Ensures the question follows platform policies before saving it to the database.
    /// </remarks>
    /// <param name="questionDto">The question data submitted by the user.</param>
    /// <returns>
    /// - <c>200 OK</c>: Returns the created question details. <br/>
    /// - <c>500 Internal Server Error</c>: If an unexpected error occurs.
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
    
    /// <summary>
    /// Retrieves all questions in the system.
    /// </summary>
    /// <param name="visibility">The visibility filter for the questions (<c>public</c> or <c>private</c>).</param>
    /// <returns>
    /// - <c>200 OK</c>: Returns a list of questions matching the visibility criteria. <br/>
    /// - <c>500 Internal Server Error</c>: If an unexpected error occurs.
    /// </returns>
    [Route("All")]
    [HttpGet]
    public async Task<IActionResult> GetQuestions(QuestionVisibility visibility)
    {
        try
        {
            var questions = await questionService.GetQuestionsByVisibilityAsync(visibility);
            logger.LogInformation("Retrieved {count} questions with visibility {visibility}", questions.Count(), visibility);
            return Ok(questions);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while getting questions.");
            return StatusCode(500, e.Message);
        }
    }
}