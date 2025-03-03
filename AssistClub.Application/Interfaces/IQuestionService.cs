using AssistClub.Application.DTOs;
using Domain.Entities;

namespace AssistClub.Application.Interfaces;

/// <summary>
/// Service interface for handling business logic related to questions.
/// This service ensures that questions are validated, processed, and formatted 
/// correctly before being stored in the database or returned to the client.
/// It acts as a bridge between the repository and the API layer.
/// </summary>
public interface IQuestionService
{
    /// <summary>
    /// Creates a new question in the system.
    /// Ensures that the question is properly structured and stored,
    /// making it available for users based on its visibility settings.
    /// </summary>
    /// <param name="questionDto">The data transfer object containing question details.</param>
    /// <returns>
    /// A <see cref="QuestionResponseDto"/> representing the created question.
    /// </returns>
    Task<QuestionResponseDto> CreateQuestionAsync(QuestionRequestDto questionDto);
}