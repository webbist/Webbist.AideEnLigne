using AssistClub.Application.DTOs;
using AssistClub.Application.Interfaces;
using Domain.Entities;

namespace AssistClub.Application.Services;

/// <summary>
/// Service responsible for handling business logic related to questions.
/// </summary>
public class QuestionService(IQuestionRepository questionRepository): IQuestionService
{
    public async Task<QuestionResponseDto> CreateQuestionAsync(QuestionRequestDto questionDto)
    {
        var question = new Question
        {
            Id = Guid.NewGuid(),
            UserId = questionDto.UserId,
            Title = questionDto.Title,
            Content = questionDto.Content,
            CreatedAt = DateTime.UtcNow,
            Visibility = questionDto.Visibility,
            Status = "open"
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