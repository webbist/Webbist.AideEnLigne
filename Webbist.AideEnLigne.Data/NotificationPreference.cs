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
    /// Represents the notification preferences for a user in the AideEnLigne application.
    /// </summary>
    public partial class NotificationPreference
    {
        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether to notify the user when an answer is published on his questions.
        /// </summary>
        public bool NotifyOnAnswerPublishedOnMyQuestion { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to notify the user when an answer to his questions are marked official.
        /// </summary>
        public bool NotifyOnAnswerToMyQuestionMarkedOfficial { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to notify on any official answer in related questions.
        /// </summary>
        public bool NotifyOnAnyOfficialAnswerInQuestionIrelated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to notify the user when one of his question or answer is modified by an admin.
        /// </summary>
        public bool NotifyOnMyQuestionOrAnswerModifiedByAdmin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to notify the user when a new answer is posted on his related questions.
        /// </summary>
        public bool NotifyOnNewAnswerInQuestionIrelated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to notify the user when a new club question is posted.
        /// </summary>
        public bool NotifyOnNewClubQuestion { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to notify the user when a related question is modified by the author.
        /// </summary>
        public bool NotifyOnQuestionIrelatedModifiedByAuthor { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public virtual User User { get; set; } = null!;

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid UserId { get; set; }
        #endregion
    }
}
