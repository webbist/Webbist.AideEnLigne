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

namespace Webbist.AideEnLigne.Data
{
    /// <summary>
    /// Represents a question in the AideEnLigne application. 
    /// </summary>
    public partial class Question
    {
        #region Properties
        /// <summary>
        /// Gets or sets the answers.
        /// </summary>
        public virtual ICollection<Answer> Answers { get; set; } = [];

        /// <summary>
        /// Gets or sets the name of the attachment.
        /// </summary>
        public string? AttachmentName { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        public virtual ICollection<Category> Categories { get; set; } = [];

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public string Content { get; set; } = null!;

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        public Guid? ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public string Status { get; set; } = null!;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; } = null!;

        /// <summary>
        /// Gets or sets the updated at.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public virtual User User { get; set; } = null!;

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the visibility.
        /// </summary>
        public string Visibility { get; set; } = null!;
        #endregion
    }
}
