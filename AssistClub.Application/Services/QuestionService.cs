using AssistClub.Application.DTOs;
using AssistClub.Application.Interfaces;
using Webbist.AideEnLigne.Data;
using Webbist.AideEnLigne.Model;

namespace AssistClub.Application.Services;

/// <inheritdoc/>
public class QuestionService(IQuestionRepository questionRepository, Notification notification, ICategoryRepository categoryRepository): IQuestionService
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
            throw new ArgumentException($"Question title exceeds the maximum character limit of {QuestionRequestDto.TitleMaxLength}.");
        }
        
        if (questionDto.Content.Length > QuestionRequestDto.ContentMaxLength)
        {
            throw new ArgumentException($"Question content exceeds the maximum character limit of {QuestionRequestDto.ContentMaxLength}.");
        }
        
        var categories = new List<Category>();
        foreach (var catName in questionDto.Categories.Distinct(StringComparer.OrdinalIgnoreCase))
        {
            var existing = await categoryRepository.GetCategoryByNameAsync(catName);
            categories.Add(existing ?? new Category
            {
                Id = Guid.NewGuid(),
                Name = catName
            });
        }
        
        var question = new Question
        {
            Id = Guid.NewGuid(),
            UserId = questionDto.UserId,
            Title = questionDto.Title,
            Content = questionDto.Content,
            CreatedAt = DateTime.UtcNow,
            Visibility = questionDto.Visibility.ToString(),
            Status = questionDto.Status,
            AttachmentName = questionDto.AttachmentName,
            Categories = categories
        };
        
        var createdQuestion = await questionRepository.CreateQuestionAsync(question);
        
        await notification.SendEmailOnCreate(createdQuestion);
        
        return new QuestionResponseDto
        {
            Title = createdQuestion.Title,
            Content = createdQuestion.Content,
            CreatedAt = createdQuestion.CreatedAt,
            Visibility = createdQuestion.Visibility,
            Status = createdQuestion.Status,
            AttachmentName = createdQuestion.AttachmentName,
            Categories = createdQuestion.Categories.Select(c => c.Name).ToList(),
        };
    }
    
    /// <summary>
    /// Retrieves all questions in the system, including user information.
    /// </summary>
    /// <returns>
    /// An <see cref="IQueryable{T}"/> of <see cref="QuestionResponseDto"/> containing the questions.
    /// </returns>
    public async Task<IQueryable<QuestionResponseDto>> GetQuestionsAsync()
    {
        var questions= await questionRepository.GetQuestions();
        return questions.Select(q => new QuestionResponseDto
        {
            Id = q.Id,
            User = new UserResponseDto
            {
                Id = q.User.Id,
                Firstname = q.User.Firstname,
                Lastname = q.User.Lastname,
                Email = q.User.Email,
                Photo = q.User.Photo,
                Club = q.User.Club,
                Microsite = q.User.Microsite
            },
            Title = q.Title,
            Content = q.Content,
            CreatedAt = q.CreatedAt,
            Visibility = q.Visibility,
            UpdatedAt = q.UpdatedAt,
            Status = q.Status,
            AttachmentName = q.AttachmentName,
            Categories = q.Categories.Select(c => c.Name).ToList()
        }).AsQueryable();
    }

    /// <summary>
    /// Retrieves a question by its unique identifier including the user who submitted it.
    /// </summary>
    /// <param name="id">The ID of the question.</param>
    /// <returns>The <see cref="QuestionResponseDto"/> if found; otherwise, <c>null</c>.</returns>
    public async Task<QuestionResponseDto?> GetQuestionByIdAsync(Guid id)
    {
        var question = await questionRepository.GetQuestionByIdAsync(id);
        if (question != null)
        {
            return new QuestionResponseDto
            {
                Id = question.Id,
                User = new UserResponseDto
                {
                    Id = question.User.Id,
                    Firstname = question.User.Firstname,
                    Lastname = question.User.Lastname,
                    Email = question.User.Email,
                    Photo = question.User.Photo,
                    Club = question.User.Club,
                    Microsite = question.User.Microsite
                },
                Title = question.Title,
                Content = question.Content,
                CreatedAt = question.CreatedAt,
                Visibility = question.Visibility,
                UpdatedAt = question.UpdatedAt,
                Status = question.Status,
                AttachmentName = question.AttachmentName,
                Categories = question.Categories.Select(c => c.Name).ToList()
            };
        }
        return null;
    }

    /// <summary>
    /// Updates an existing question in the system.
    /// </summary>
    /// <param name="id">The ID of the question to update.</param>
    /// <param name="questionRequest">The <see cref="QuestionRequestDto"/> containing updated question details.</param>
    /// <returns>
    /// A <c>bool</c> indicating whether the update was successful or not.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if the question title exceeds <see cref="QuestionRequestDto.TitleMaxLength"/> characters
    /// or the content exceeds <see cref="QuestionRequestDto.ContentMaxLength"/> characters.
    /// </exception>
    public async Task<bool> UpdateQuestionAsync(Guid id, QuestionRequestDto questionRequest)
    {
        if (questionRequest.Title.Length > QuestionRequestDto.TitleMaxLength)
        {
            throw new ArgumentException($"Question title exceeds the maximum character limit of {QuestionRequestDto.TitleMaxLength}.");
        }
        
        if (questionRequest.Content.Length > QuestionRequestDto.ContentMaxLength)
        {
            throw new ArgumentException($"Question content exceeds the maximum character limit of {QuestionRequestDto.ContentMaxLength}.");
        }
        
        var question = new Question
        {
            Id = id,
            UserId = questionRequest.UserId,
            Title = questionRequest.Title,
            Content = questionRequest.Content,
            UpdatedAt = DateTime.UtcNow,
            Visibility = questionRequest.Visibility.ToString(),
            AttachmentName = questionRequest.AttachmentName,
            ModifiedBy = questionRequest.ModifiedBy,
            Categories = questionRequest.Categories.Select(c => new Category
            {
                Name = c
            }).ToList()
        };
        var result = await questionRepository.UpdateQuestionAsync(question);
        if (result)
        {
            await notification.SendEmailOnUpdateQuestion(question);
        }
        return result;
    }
    
    /// <summary>
    /// Deletes a question from the system.
    /// </summary>
    /// <param name="id">The ID of the question to delete.</param>
    /// <returns>
    /// A <c>bool</c> indicating whether the deletion was successful or not (<c>true</c> if successful, <c>false</c> otherwise).
    /// </returns>
    public async Task<bool> DeleteQuestionAsync(Guid id)
    {
        return await questionRepository.DeleteQuestionAsync(id);
    }
}