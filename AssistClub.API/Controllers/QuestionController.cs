using AssistClub.Application.DTOs;
using AssistClub.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AssistClub.API.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class QuestionController(IQuestionService questionService, ILogger<QuestionController> logger) : ControllerBase
{
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