using AssistClub.Application.DTOs;
using AssistClub.Application.Interfaces;
using Domain.Entities;

namespace AssistClub.Application.Services;

/// <inheritdoc/>
public class QuestionService(IQuestionRepository questionRepository): IQuestionService
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
    public async Task<QuestionResponseDto> CreateQuestionAsync(QuestionRequestDto questionDto)
    {
        if (questionDto.Title.Length > QuestionRequestDto.TitleMaxLength)
        {
            throw new ArgumentException("Question title exceeds the maximum character limit of 255.");
        }
        
        if (questionDto.Content.Length > QuestionRequestDto.ContentMaxLength)
        {
            throw new ArgumentException("Question content exceeds the maximum character limit of 2000.");
        }
        
        var question = new Question
        {
            Id = Guid.NewGuid(),
            UserId = questionDto.UserId,
            Title = questionDto.Title,
            Content = questionDto.Content,
            CreatedAt = DateTime.UtcNow,
            Visibility = questionDto.Visibility.ToString().ToLower(),
            Status = questionDto.Status
        };
        
        var createdQuestion = await questionRepository.CreateQuestionAsync(question);
        
        return new QuestionResponseDto
        {
            UserId = createdQuestion.UserId,
            Title = createdQuestion.Title,
            Content = createdQuestion.Content,
            CreatedAt = createdQuestion.CreatedAt,
            Visibility = createdQuestion.Visibility,
            Status = createdQuestion.Status
        };
    }
}