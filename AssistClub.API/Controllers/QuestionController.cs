using AssistClub.Application.DTOs;
using AssistClub.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

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
    /// This endpoint allows users to submit questions to the system.
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
            logger.LogTrace("Question created with title {title}", question.Title);
            return Ok(question);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while creating the question.");
            return StatusCode(500, e.Message);
        }
    }
    
    /// <summary>
    /// Retrieves all questions in the system with OData support.
    /// </summary>
    /// <remarks>
    /// Supported OData parameters include:
    /// <para><c>$filter</c>: Allows filtering questions by attributes (e.g., <c>$filter=Visibility eq 'public'</c>).</para>
    /// <para><c>$orderby</c>: Enables sorting questions (e.g., <c>$orderby=CreatedAt desc</c>).</para>
    /// <para><c>$select</c>: Retrieves only specific fields (e.g., <c>$select=Title,CreatedAt</c>).</para>
    /// <para><c>$expand</c>: Includes related entities, such as user details (e.g., <c>$expand=User</c>).</para>
    /// </remarks>
    /// <returns>
    /// <c>200 OK</c>: Returns a list of questions.<br/>
    /// <c>500 Internal Server Error</c>: If an unexpected error occurs.
    /// </returns>
    [Route("All")]
    [HttpGet]
    [EnableQuery]
    public async Task<IActionResult> GetQuestions()
    {
        try
        {
            var questions = await questionService.GetQuestionsAsync();
            return Ok(questions);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while getting questions.");
            return StatusCode(500, e.Message);
        }
    }
    
    /// <summary>
    /// Retrieves a question by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the question.</param>
    /// <returns>
    /// <c>200 OK</c>: Returns the question details.<br/>
    /// <c>500 Internal Server Error</c>: If an unexpected error occurs.
    /// </returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetQuestion(Guid id)
    {
        try
        {
            var question = await questionService.GetQuestionByIdAsync(id);
            return Ok(question);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while getting the question.");
            return StatusCode(500, e.Message);
        }
    }
}