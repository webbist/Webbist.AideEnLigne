using AssistClub.Application.DTOs;
using AssistClub.Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

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
    
    /// <summary>
    /// Retrieves all questions in the system filtered by visibility.
    /// </summary>
    /// <param name="visibility">
    /// The <see cref="QuestionVisibility"/> filter to apply when retrieving questions.
    /// </param>
    /// <returns>
    /// A collection of <see cref="QuestionResponseDto"/> representing the filtered questions.
    /// </returns>
    public async Task<IEnumerable<QuestionResponseDto>> GetQuestionsByVisibilityAsync(QuestionVisibility visibility)
    {
        var questions = await questionRepository.GetQuestions(visibility)
            .OrderByDescending(q => q.CreatedAt)
            .Select(q => new QuestionResponseDto
            {
                UserId = q.UserId,
                UserFullname = q.User.Firstname + " " + q.User.Lastname,
                UserPhoto = q.User.Photo,
                Title = q.Title,
                Content = q.Content,
                CreatedAt = q.CreatedAt,
                Visibility = q.Visibility,
                Status = q.Status
            }).ToListAsync();
        return questions;
    }
}