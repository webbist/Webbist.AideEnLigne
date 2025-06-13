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
using Webbist.AideEnLigne.Services.Questions;
using Webbist.AideEnLigne.Services.Users;

namespace Webbist.AideEnLigne.Services.Answers
{
    /// <inheritdoc/>
    public class AnswerService(IAnswerRepository answerRepository, Notification notification): IAnswerService
    {
        #region Methods
        /// <summary>
        /// Creates a new answer in the system.
        /// </summary>
        /// <remarks>
        /// Validates the input and delegates the persistence to the repository.
        /// Returns a simplified view model for external use.
        /// </remarks>
        /// <param name="answerRequest">The <see cref="AnswerRequest"/> containing answer details.</param>
        /// <returns>A <see cref="AnswerResponse"/> representing the created answer.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the answer content exceeds <see cref="AnswerRequest.ContentMaxLength"/> characters.
        /// </exception>
        public async Task<AnswerResponse> CreateAnswerAsync(AnswerRequest answerRequest)
        {
            if (answerRequest.Content.Length > AnswerRequest.ContentMaxLength)
            {
                throw new ArgumentException($"Answer content exceeds the maximum character limit of {AnswerRequest.ContentMaxLength}.");
            }
        
            var answer = new Answer
            {
                Id = Guid.NewGuid(),
                QuestionId = answerRequest.QuestionId,
                UserId = answerRequest.UserId,
                Content = answerRequest.Content,
                Status = AnswerStatus.Pending.ToString(),
                CreatedAt = DateTime.UtcNow,
                AttachmentName = answerRequest.AttachmentName
            };
        
            var createdAnswer = await answerRepository.CreateAnswerAsync(answer);
        
            await notification.SendEmailOnNewAnswer(createdAnswer);
        
            return new AnswerResponse
            {
                Id = createdAnswer.Id,
                Question = new QuestionResponse
                {
                    Id = createdAnswer.Question.Id,
                    Title = createdAnswer.Question.Title,
                    CreatedAt = createdAnswer.Question.CreatedAt,
                    UpdatedAt = createdAnswer.Question.UpdatedAt,
                    Visibility = createdAnswer.Question.Visibility,
                    Status = createdAnswer.Question.Status
                },
                Content = createdAnswer.Content,
                Status = createdAnswer.Status,
                CreatedAt = createdAnswer.CreatedAt,
                UpdatedAt = createdAnswer.UpdatedAt,
                AttachmentName = answerRequest.AttachmentName
            };
        }

        /// <summary>
        /// Retrieves all answers in the system, including user information.
        /// </summary>
        /// <param name="userId">The ID of the user to check if he voted for any answer.</param>
        /// <returns>
        /// An <see cref="IQueryable{T}"/> of <see cref="AnswerResponse"/> containing the answers.
        /// </returns>
        public async Task<IQueryable<AnswerResponse>> GetAnswersAsync(Guid userId)
        {
            var answers = await answerRepository.GetAnswers();
            return answers.Select(a => new AnswerResponse
                {
                    Id = a.Id,
                    Question = new QuestionResponse
                    {
                        Id = a.Question.Id,
                        User = new UserResponse
                        {
                            Id = a.Question.User.Id
                        },
                        Title = a.Question.Title,
                        CreatedAt = a.Question.CreatedAt,
                        UpdatedAt = a.Question.UpdatedAt,
                        Visibility = a.Question.Visibility,
                        Status = a.Question.Status,
                    },
                    User = new UserResponse
                    {
                        Id = a.User.Id,
                        Firstname = a.User.Firstname,
                        Lastname = a.User.Lastname,
                        Email = a.User.Email,
                        Photo = a.User.Photo,
                        Club = a.User.Club,
                        Microsite = a.User.Microsite
                    },
                    Content = a.Content,
                    Status = a.Status,
                    CreatedAt = a.CreatedAt,
                    UpdatedAt = a.UpdatedAt,
                    AttachmentName = a.AttachmentName,
                    VoteCount = a.AnswerVotes.Count,
                    HasVoted = a.AnswerVotes.Any(v => v.UserId == userId)
                }
            ).AsQueryable();
        }

        /// <summary>
        /// Updates the status of an answer and the associated question status.
        /// </summary>
        /// <param name="id">The unique identifier of the answer to be updated.</param>
        /// <param name="newStatus">The new status to be set for the answer.</param>
        /// <returns>
        /// Returns <c>true</c> if the update was successful; otherwise, <c>false</c>.
        /// </returns>
        public async Task<bool> UpdateAnswerStatusAsync(Guid id, AnswerStatus newStatus)
        {
            var updateAnswer = await answerRepository.UpdateAnswerStatusAsync(id, newStatus);
            if (updateAnswer == null) return false;
            if (newStatus == AnswerStatus.Official) await notification.SendEmailOnOfficialAnswer(updateAnswer);
            return true;
        }

        /// <summary>
        /// Updates an existing answer in the database.
        /// </summary>
        /// <param name="id">The unique identifier of the answer to be updated.</param>
        /// <param name="answerRequest">The <see cref="AnswerRequest"/> containing the updated answer details.</param>
        /// <returns>
        /// A boolean indicating whether the update was successful (<c>true</c> if successful, <c>false</c> otherwise).
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the answer content exceeds <see cref="AnswerRequest.ContentMaxLength"/> characters.
        /// </exception>
        public async Task<bool> UpdateAnswerAsync(Guid id, AnswerRequest answerRequest)
        {
            if (answerRequest.Content.Length > AnswerRequest.ContentMaxLength)
            {
                throw new ArgumentException($"Answer content exceeds the maximum character limit of {AnswerRequest.ContentMaxLength}.");
            }
            var updatedAnswer = new Answer
            {
                Id = id,
                UserId = answerRequest.UserId,
                Content = answerRequest.Content,
                UpdatedAt = DateTime.UtcNow,
                AttachmentName = answerRequest.AttachmentName
            };
            var result = await answerRepository.UpdateAnswerAsync(updatedAnswer);
            if (result)
            {
                await notification.SendEmailOnUpdateAnswer(updatedAnswer);
            }
            return result;
        }

        /// <summary>
        /// Deletes an answer from the system.
        /// </summary>
        /// <param name="answerId">The ID of the answer to be deleted.</param>
        /// <returns>
        /// A boolean indicating whether the deletion was successful (<c>true</c> if successful, <c>false</c> otherwise).
        /// </returns>
        public async Task<bool> DeleteAnswerAsync(Guid answerId)
        {
            return await answerRepository.DeleteAnswerAsync(answerId);
        }
        #endregion
    }
}