// Copyright (C) 2025 Webbist
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, see <https://www.gnu.org/licenses/old-licenses/gpl-2.0.html>.

using Webbist.AideEnLigne.Data;
using Webbist.AideEnLigne.Model;
using Webbist.AideEnLigne.Services.Notifications;
using Webbist.AideEnLigne.Services.Users;

namespace Webbist.AideEnLigne.Services.Questions
{
    /// <inheritdoc/>
    public class QuestionService(IQuestionRepository questionRepository, Notification notification, ICategoryRepository categoryRepository): IQuestionService
    {
        #region Methods
        /// <summary>
        /// Creates a new question in the system.
        /// </summary>
        /// <remarks>
        /// Ensures that the question is properly structured and stored,
        /// making it available for users based on its visibility settings.
        /// </remarks>
        /// <param name="questionRequest">The <see cref="QuestionRequest"/> containing question details.</param>
        /// <returns>
        /// A <see cref="QuestionResponse"/> representing the created question.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the question title exceeds <see cref="QuestionRequest.TitleMaxLength"/> characters 
        /// or the content exceeds <see cref="QuestionRequest.ContentMaxLength"/> characters.
        /// </exception>
        public async Task<QuestionResponse> CreateQuestionAsync(QuestionRequest questionRequest)
        {
            if (questionRequest.Title.Length > QuestionRequest.TitleMaxLength)
            {
                throw new ArgumentException($"Question title exceeds the maximum character limit of {QuestionRequest.TitleMaxLength}.");
            }
        
            if (questionRequest.Content.Length > QuestionRequest.ContentMaxLength)
            {
                throw new ArgumentException($"Question content exceeds the maximum character limit of {QuestionRequest.ContentMaxLength}.");
            }
        
            var categories = new List<Category>();
            foreach (var catName in questionRequest.Categories.Distinct(StringComparer.OrdinalIgnoreCase))
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
                UserId = questionRequest.UserId,
                Title = questionRequest.Title,
                Content = questionRequest.Content,
                CreatedAt = DateTime.UtcNow,
                Visibility = questionRequest.Visibility.ToString(),
                Status = questionRequest.Status,
                AttachmentName = questionRequest.AttachmentName,
                Categories = categories
            };
        
            var createdQuestion = await questionRepository.CreateQuestionAsync(question);
        
            await notification.SendEmailOnCreate(createdQuestion);
        
            return new QuestionResponse
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
        /// An <see cref="IQueryable{T}"/> of <see cref="QuestionResponse"/> containing the questions.
        /// </returns>
        public async Task<IQueryable<QuestionResponse>> GetQuestionsAsync()
        {
            var questions= await questionRepository.GetQuestions();
            return questions.Select(q => new QuestionResponse
            {
                Id = q.Id,
                User = new UserResponse
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
        /// <returns>The <see cref="QuestionResponse"/> if found; otherwise, <c>null</c>.</returns>
        public async Task<QuestionResponse?> GetQuestionByIdAsync(Guid id)
        {
            var question = await questionRepository.GetQuestionByIdAsync(id);
            if (question != null)
            {
                return new QuestionResponse
                {
                    Id = question.Id,
                    User = new UserResponse
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
        /// <param name="questionRequest">The <see cref="QuestionRequest"/> containing updated question details.</param>
        /// <returns>
        /// A <c>bool</c> indicating whether the update was successful or not.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the question title exceeds <see cref="QuestionRequest.TitleMaxLength"/> characters
        /// or the content exceeds <see cref="QuestionRequest.ContentMaxLength"/> characters.
        /// </exception>
        public async Task<bool> UpdateQuestionAsync(Guid id, QuestionRequest questionRequest)
        {
            if (questionRequest.Title.Length > QuestionRequest.TitleMaxLength)
            {
                throw new ArgumentException($"Question title exceeds the maximum character limit of {QuestionRequest.TitleMaxLength}.");
            }
        
            if (questionRequest.Content.Length > QuestionRequest.ContentMaxLength)
            {
                throw new ArgumentException($"Question content exceeds the maximum character limit of {QuestionRequest.ContentMaxLength}.");
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
        #endregion
    }
}