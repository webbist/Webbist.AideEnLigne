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
    /// Retrieves all questions in the system, including user information.
    /// </summary>
    /// <returns>
    /// An <see cref="IQueryable{T}"/> of <see cref="QuestionResponseDto"/> containing the questions.
    /// </returns>
    Task<IQueryable<QuestionResponseDto>> GetQuestionsAsync();
    
    /// <summary>
    /// Retrieves a question by its unique identifier including the user who submitted it.
    /// </summary>
    /// <param name="id">The ID of the question.</param>
    /// <returns>The <see cref="QuestionResponseDto"/> if found; otherwise, <c>null</c>.</returns>
    Task<QuestionResponseDto?> GetQuestionByIdAsync(Guid id);
}