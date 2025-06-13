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
    /// Represents the possible visibility states of a question.
    /// </summary>
    public enum QuestionVisibility
    {
        /// <summary>
        /// Indicates that the question is visible to all users.
        /// </summary>
        Public,
    
        /// <summary>
        /// Indicates that the question is only visible to the user who created it and the admin.
        /// </summary>
        Private
    }
}