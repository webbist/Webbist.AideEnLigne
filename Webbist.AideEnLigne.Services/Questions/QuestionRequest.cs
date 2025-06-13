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

namespace Webbist.AideEnLigne.Services.Questions
{
    /// <summary>
    /// Represents the request structure for creating or updating a question.
    /// </summary>
    /// <remarks>
    /// Ensures that only valid and necessary data is received from the client before processing.
    /// </remarks>
    public class QuestionRequest
    {
        #region Constants
        /// <summary>
        /// Maximum allowed length for the title.
        /// </summary>
        public const int TitleMaxLength = 255;

        /// <summary>
        /// Maximum allowed length for the content.
        /// </summary>
        public const int ContentMaxLength = 2000;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the unique identifier of the user submitting the question.
        /// </summary>
        public Guid UserId { get; set; }
    
        /// <summary>
        /// Gets or sets the title of the question.
        /// </summary>
        public string Title { get; set; }
    
        /// <summary>
        /// Gets or sets the full content of the question.
        /// </summary>
        public string Content { get; set; }
    
        /// <summary>
        /// Gets or sets the visibility of the question (<c>public</c> or <c>private</c>).
        /// </summary>
        public QuestionVisibility Visibility { get; set; }
    
        /// <summary>
        /// Gets or sets the status of the question (<c>open</c>, <c>pending</c>, or <c>resolved</c>).
        /// </summary>
        public string Status { get; set; }
    
        /// <summary>
        /// Gets or sets the name of the uploaded attachment file.
        /// </summary>
        public string? AttachmentName { get; set; }
    
        /// <summary>
        /// Gets or sets the unique identifier of the user who last modified the question.
        /// </summary>
        public Guid? ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the list of categories associated with the question.
        /// </summary>
        public IEnumerable<string> Categories { get; set; } = [];
        #endregion
    }
}