using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace AssistClub.API.Controllers;

/// <summary>
/// Represents the controller responsible for managing file-related API endpoints.
/// </summary>
/// <param name="webHostEnvironment">The environment information for the web host.</param>
[ApiController]
[Route("v1/files")]
public class FileController(IWebHostEnvironment webHostEnvironment) : ControllerBase
{
    /// <summary>
    /// Maximum file size allowed for uploads (10 MB).
    /// </summary>
    const long MaxFileSize = 10 * 1024 * 1024;
    
    /// <summary>
    /// Uploads a file to the server.
    /// </summary>
    /// <param name="file">The file to be uploaded.</param>
    /// <returns>
    /// - <c>200 OK</c>: Returns the file path if successful. <br/>
    /// - <c>400 Bad Request</c>: If the file is null, empty, or exceeds the size limit. <br/>
    /// - <c>500 Internal Server Error</c>: If an unexpected error occurs.
    /// </returns>
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0) return BadRequest("No file uploaded.");

            var allowedExtensions = new[] { ".pdf", ".doc", ".docx", ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension)) return BadRequest("File extension not allowed.");

            if (file.Length > MaxFileSize) return BadRequest("File too large.");

            var uploadsPath = Path.Combine(webHostEnvironment.ContentRootPath, "uploads");
            if (!Directory.Exists(uploadsPath)) Directory.CreateDirectory(uploadsPath);

            var newFileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsPath, newFileName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            Console.WriteLine("File uploaded successfully");
            return Ok(new { filePath = newFileName });
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error uploading file: {e.Message}");
            return StatusCode(500, "Internal server error.");
        }
    }
    
    /// <summary>
    /// Downloads a file from the server.
    /// </summary>
    /// <param name="filename">The name of the file to be downloaded.</param>
    /// <returns>
    /// - Returns the file if found. <br/>
    /// - <c>404 Not Found</c>: If the file does not exist. <br/>
    /// - <c>500 Internal Server Error</c>: If an unexpected error occurs.
    /// </returns>
    [HttpGet("download/{filename}")]
    public IActionResult Download(string filename)
    {
        try
        {
            var path = Path.Combine(webHostEnvironment.ContentRootPath, "uploads", filename);
            if (!System.IO.File.Exists(path)) return NotFound();

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            Console.WriteLine("File downloaded successfully");
            return PhysicalFile(path, contentType, filename);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error downloading file: {e.Message}");
            return StatusCode(500, "Internal server error.");
        }
    }
    
    /// <summary>
    /// Deletes a file from the server.
    /// </summary>
    /// <param name="filename">The name of the file to be deleted.</param>
    /// <returns>
    /// - <c>200 OK</c>: If the file was successfully deleted. <br/>
    /// - <c>404 Not Found</c>: If the file does not exist. <br/>
    /// - <c>500 Internal Server Error</c>: If an unexpected error occurs.
    /// </returns>
    [HttpDelete("delete/{filename}")]
    public IActionResult DeleteFile(string filename)
    {
        try
        {
            var path = Path.Combine(webHostEnvironment.ContentRootPath, "uploads", filename);
            if (!System.IO.File.Exists(path)) return NotFound();

            System.IO.File.Delete(path);
            Console.WriteLine("File deleted successfully");
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error deleting file: {e.Message}");
            return StatusCode(500, "Internal server error.");
        }
    }
}