using AssistClub.Application.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace AssistClub.Application.Interfaces;

/// <summary>
/// Represents the service interface for handling business logic related to questions.
/// </summary>
/// <remarks>
/// Ensures that questions are validated, processed, and formatted 
/// correctly before being stored in the database or returned to the client.
/// Acts as a bridge between the repository and the API layer.
/// </remarks>
public interface IQuestionService
{
    /// <summary>
    /// Creates a new question in the system.
    /// </summary>
    /// <remarks>
    /// Ensures that the question is properly structured and stored,
    /// making it available for users based on its visibility settings.
    /// </remarks>
    /// <param name="questionDto">The <see cref="QuestionRequestDto"/> containing question details.</param>
    /// <returns>
    /// A <see cref="QuestionResponseDto"/> representing the created question.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if the question title exceeds <see cref="QuestionRequestDto.TitleMaxLength"/> characters 
    /// or the content exceeds <see cref="QuestionRequestDto.ContentMaxLength"/> characters.
    /// </exception>
    Task<QuestionResponseDto> CreateQuestionAsync(QuestionRequestDto questionDto);
    
    /// <summary>
    /// Retrieves all questions in the system filtered by visibility.
    /// </summary>
    /// <param name="visibility">
    /// The <see cref="QuestionVisibility"/> filter to apply when retrieving questions.
    /// </param>
    /// <returns>
    /// A collection of <see cref="QuestionResponseDto"/> representing the filtered questions.
    /// </returns>
    Task<IEnumerable<QuestionResponseDto>> GetQuestionsByVisibilityAsync(QuestionVisibility visibility);
}