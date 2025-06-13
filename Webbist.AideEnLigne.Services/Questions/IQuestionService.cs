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

namespace Webbist.AideEnLigne.Services.Questions
{
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
        Task<QuestionResponse> CreateQuestionAsync(QuestionRequest questionRequest);
    
        /// <summary>
        /// Retrieves all questions in the system, including user information.
        /// </summary>
        /// <returns>
        /// An <see cref="IQueryable{T}"/> of <see cref="QuestionResponse"/> containing the questions.
        /// </returns>
        Task<IQueryable<QuestionResponse>> GetQuestionsAsync();
    
        /// <summary>
        /// Retrieves a question by its unique identifier including the user who submitted it.
        /// </summary>
        /// <param name="id">The ID of the question.</param>
        /// <returns>The <see cref="QuestionResponse"/> if found; otherwise, <c>null</c>.</returns>
        Task<QuestionResponse?> GetQuestionByIdAsync(Guid id);

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
        Task<bool> UpdateQuestionAsync(Guid id, QuestionRequest questionRequest);

        /// <summary>
        /// Deletes a question from the system.
        /// </summary>
        /// <param name="id">The ID of the question to delete.</param>
        /// <returns>
        /// A <c>bool</c> indicating whether the deletion was successful or not (<c>true</c> if successful, <c>false</c> otherwise).
        /// </returns>
        Task<bool> DeleteQuestionAsync(Guid id);
        #endregion
    }
}