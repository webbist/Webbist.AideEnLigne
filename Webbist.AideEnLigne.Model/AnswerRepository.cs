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

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Webbist.AideEnLigne.Data;

namespace Webbist.AideEnLigne.Model
{
    /// <inheritdoc/>
    public class AnswerRepository(DataContext db, ILogger<AnswerRepository> logger) : IAnswerRepository
    {
        #region Methods
        /// <summary>
        /// Adds a new answer to the database and updates the status of the associated question.
        /// </summary>
        /// <param name="answer">The <see cref="Answer"/> entity to add.</param>
        /// <returns>The <see cref="Answer"/> entity that was added.</returns>
        /// <exception cref="DbUpdateException">Thrown if an error occurs while saving the answer to the database.</exception>
        public async Task<Answer> CreateAnswerAsync(Answer answer)
        {
            try
            {
                var question = await db.Questions.FindAsync(answer.QuestionId);
                if (question != null && question.Status == QuestionStatus.Open.ToString())
                {
                    question.Status = QuestionStatus.Pending.ToString();
                    db.Questions.Update(question);
                }
                var result = db.Answers.Add(answer);
                await db.SaveChangesAsync();
                return result.Entity;
            }
            catch (DbUpdateException ex)
            {
                logger.LogError(ex, "An error occurred while adding a new answer to the database.");
                throw;
            }
        }

        /// <summary>
        /// Retrieves all answers from the database, allowing further filtering, sorting, and pagination.
        /// </summary>
        /// <returns>
        /// An <see cref="IQueryable{T}"/> representing the answers in the database.
        /// </returns>
        public async Task<IQueryable<Answer>> GetAnswers()
        {
            return await Task.FromResult(db.Answers);
        }

        /// <summary>
        /// Updates the status of an answer and the associated question status.
        /// </summary>
        /// <param name="answerId">The unique identifier of the answer to be updated.</param>
        /// <param name="newStatus">The new status to be set for the answer.</param>
        /// <returns>
        /// Returns the updated <see cref="Answer"/> entity if successful; otherwise, <c>null</c>.
        /// </returns>
        /// <exception cref="DbUpdateException">Thrown if an error occurs while saving the answer to the database.</exception>
        public async Task<Answer?> UpdateAnswerStatusAsync(Guid answerId, AnswerStatus newStatus)
        {
            try
            {
                var answer = await db.Answers.FindAsync(answerId);
                if (answer == null) return null;

                answer.Status = newStatus.ToString();
                var updatedAnswer = db.Answers.Update(answer);

                var question = await db.Questions.FindAsync(answer.QuestionId);
                if (question != null)
                {
                    question.Status = newStatus == AnswerStatus.Official
                        ? QuestionStatus.Resolved.ToString()
                        : QuestionStatus.Pending.ToString();
                    db.Questions.Update(question);
                }
                await db.SaveChangesAsync();
                return updatedAnswer.Entity;
            }
            catch (DbUpdateException e)
            {
                logger.LogError(e, "An error occurred while updating the official status of the answer.");
                throw;
            }
        }

        /// <summary>
        /// Updates an existing answer in the database.
        /// </summary>
        /// <param name="updatedAnswer">The <see cref="Answer"/> entity to update.</param>
        /// <returns>
        /// Returns <c>true</c> if the update was successful; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="DbUpdateException">Thrown if an error occurs while saving the answer to the database.</exception>
        public async Task<bool> UpdateAnswerAsync(Answer updatedAnswer)
        {
            try
            {
                var answer = await db.Answers.FindAsync(updatedAnswer.Id);
                if (answer == null) return false;
                answer.Content = updatedAnswer.Content;
                answer.UpdatedAt = updatedAnswer.UpdatedAt;
                answer.AttachmentName = updatedAnswer.AttachmentName;
                db.Answers.Update(answer);
                return await db.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException e)
            {
                logger.LogError(e, "An error occurred while updating the answer.");
                throw;
            }
        }

        /// <summary>
        /// Deletes an answer from the database by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the answer to be deleted.</param>
        /// <returns>
        /// Returns <c>true</c> if the deletion was successful; otherwise, <c>false</c>.
        /// </returns>
        public async Task<bool> DeleteAnswerAsync(Guid id)
        {
            var answer = await db.Answers.FindAsync(id);
            if (answer == null)
            {
                logger.LogInformation("Attempted to delete answer with ID {Id}, but it was not found in the database.", id);
                return true;
            }
            db.Answers.Remove(answer);
            var result = await db.SaveChangesAsync();

            var remainingAnswers = await db.Answers
                .Where(a => a.QuestionId == answer.QuestionId)
                .ToListAsync();

            var question = await db.Questions.FindAsync(answer.QuestionId);
            if (question != null)
            {
                Console.WriteLine(remainingAnswers.Count);
                if (remainingAnswers.Count == 0)
                {
                    question.Status = QuestionStatus.Open.ToString();
                }
                else if (answer.Status == AnswerStatus.Official.ToString())
                {
                    question.Status = QuestionStatus.Pending.ToString();
                }
                db.Questions.Update(question);
                await db.SaveChangesAsync();
            }
            return result == 1;
        }
        #endregion
    }
}