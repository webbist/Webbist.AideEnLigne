using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Webbist.AideEnLigne.Model;

namespace AssistClub.API.Controllers;

/// <summary>
/// Represents the controller responsible for managing category-related API endpoints.
/// </summary>
/// <param name="categoryRepository">The repository responsible for category management.</param>
/// <param name="logger">The logging service.</param>
[ApiController]
[Route("v1/[controller]")]
public class CategoryController(ICategoryRepository categoryRepository, ILogger<AnswerController> logger) : ControllerBase
{
    /// <summary>
    /// Retrieves all categories in the system with OData support.
    /// </summary>
    /// <returns>
    /// <c>200 OK</c>: Returns a list of categories.<br/>
    /// <c>500 Internal Server Error</c>: If an unexpected error occurs.
    /// </returns>
    [HttpGet]
    [EnableQuery]
    public async Task<IActionResult> GetAllCategories()
    {
        try
        {
            var categories = await categoryRepository.GetCategoriesAsync();
            return Ok(categories);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while retrieving categories.");
            return StatusCode(500, e.Message);
        }
    }
}