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

namespace Webbist.AideEnLigne.Services.Users
{
    /// <summary>
    /// Represents a request to set notification preferences for a user.
    /// </summary>
    public class NotificationPreferenceRequest
    {
        #region Properties
        /// <summary>
        /// Gets or sets the unique identifier for the user whose notification preferences are being set.
        /// </summary>
        public Guid UserId { get; set; }
    
        /// <summary>
        /// Gets or sets a value indicating whether the user wants to be notified about new questions in their club.
        /// </summary>
        public bool NotifyOnNewClubQuestion { get; set; }
    
        /// <summary>
        /// Gets or sets a value indicating whether the user wants to be notified about new answers on their question.
        /// </summary>
        public bool NotifyOnAnswerPublishedOnMyQuestion { get; set; }
    
        /// <summary>
        /// Gets or sets a value indicating whether the user wants to be notified when an answer to their question
        /// is marked as official.
        /// </summary>
        public bool NotifyOnAnswerToMyQuestionMarkedOfficial { get; set; }
    
        /// <summary>
        /// Gets or sets a value indicating whether the user wants to be notified when their question or answer
        /// is modified by an admin.
        /// </summary>
        public bool NotifyOnMyQuestionOrAnswerModifiedByAdmin { get; set; }
    
        /// <summary>
        /// Gets or sets a value indicating whether the user wants to be notified about any official
        /// answer in questions they are related to.
        /// </summary>
        public bool NotifyOnAnyOfficialAnswerInQuestionIrelated { get; set; }
    
        /// <summary>
        /// Gets or sets a value indicating whether the user wants to be notified when a question
        /// they are related to is modified by the author.
        /// </summary>
        public bool NotifyOnQuestionIrelatedModifiedByAuthor { get; set; }
    
        /// <summary>
        /// Gets or sets a value indicating whether the user wants to be notified about new answers
        /// in questions they are related to.
        /// </summary>
        public bool NotifyOnNewAnswerInQuestionIrelated { get; set; }
        #endregion
    }
}