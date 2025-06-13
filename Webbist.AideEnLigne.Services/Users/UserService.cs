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

namespace Webbist.AideEnLigne.Services.Users
{
    /// <inheritdoc/>
    public class UserService(IUserRepository userRepository) : IUserService
    {
        #region Methods
        /// <summary>
        /// Retrieves a user's profile information using their email address.
        /// </summary>
        /// <param name="email">The email of the user to retrieve.</param>
        /// <returns>
        /// A <see cref="UserResponse"/> containing user details if found; otherwise, <c>null</c>.
        /// </returns>
        public async Task<UserResponse?> GetUserByEmailAsync(string email)
        {
            var user = await userRepository.GetUserByEmailAsync(email);
            if (user != null)
                return new UserResponse
                {
                    Id = user.Id,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Email = user.Email,
                    Photo = user.Photo,
                    Club = user.Club,
                    Microsite = user.Microsite,
                    Role = user.Role
                };
            return null;
        }

        /// <summary>
        /// Creates a new user in the system.
        /// </summary>
        /// <param name="user">The user details to create.</param>
        /// <returns>A <see cref="UserResponse"/> containing the created user details.</returns>
        public async Task<UserResponse> CreateUserAsync(UserRequest user)
        {
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Photo = user.Photo,
                Club = user.Club,
                Microsite = user.Microsite,
                Role = user.Role.ToString().ToLower()
            };
        
            var createdUser = await userRepository.CreateUserAsync(newUser);

            return new UserResponse
            {
                Id = createdUser.Id,
                Firstname = createdUser.Firstname,
                Lastname = createdUser.Lastname,
                Email = createdUser.Email,
                Photo = createdUser.Photo,
                Club = createdUser.Club,
                Microsite = createdUser.Microsite,
                Role = createdUser.Role
            };
        }
    
        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>
        /// A <see cref="UserResponse"/> containing user details if found; otherwise, <c>null</c>.
        /// </returns>
        public async Task<UserResponse?> GetUserByIdAsync(Guid id)
        {
            var user = await userRepository.GetUserByIdAsync(id);
            if (user != null)
                return new UserResponse
                {
                    Id = user.Id,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Email = user.Email,
                    Photo = user.Photo,
                    Club = user.Club,
                    Microsite = user.Microsite,
                    Role = user.Role
                };
            return null;
        }
    
        /// <summary>
        /// Retrieves the notification preferences for a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user whose notification preferences to retrieve.</param>
        /// <returns>
        /// A <see cref="NotificationPreferenceRequest"/> containing the user's notification preferences if found; otherwise, <c>null</c>.
        /// </returns>
        public async Task<NotificationPreferenceRequest?> GetUserNotificationPreferencesAsync(Guid userId)
        {
            var preferences = await userRepository.GetUserNotificationPreferencesAsync(userId);
            if (preferences != null)
                return new NotificationPreferenceRequest
                {
                    UserId = preferences.UserId,
                    NotifyOnNewClubQuestion = preferences.NotifyOnNewClubQuestion,
                    NotifyOnAnswerPublishedOnMyQuestion = preferences.NotifyOnAnswerPublishedOnMyQuestion,
                    NotifyOnAnswerToMyQuestionMarkedOfficial = preferences.NotifyOnAnswerToMyQuestionMarkedOfficial,
                    NotifyOnMyQuestionOrAnswerModifiedByAdmin = preferences.NotifyOnMyQuestionOrAnswerModifiedByAdmin,
                    NotifyOnAnyOfficialAnswerInQuestionIrelated = preferences.NotifyOnAnyOfficialAnswerInQuestionIrelated,
                    NotifyOnQuestionIrelatedModifiedByAuthor = preferences.NotifyOnQuestionIrelatedModifiedByAuthor,
                    NotifyOnNewAnswerInQuestionIrelated = preferences.NotifyOnNewAnswerInQuestionIrelated
                };
            return null;
        }

        /// <summary>
        /// Updates the notification preferences for a user.
        /// </summary>
        /// <param name="preferences">The <see cref="NotificationPreferenceRequest"/> containing the updated preferences.</param>
        /// <returns>
        /// Returns <c>true</c> if the update was successful; otherwise, <c>false</c>.
        /// </returns>
        public async Task<bool> UpdateUserNotificationPreferencesAsync(NotificationPreferenceRequest preferences)
        {
            var updatedPreferences = new NotificationPreference
            {
                UserId = preferences.UserId,
                NotifyOnNewClubQuestion = preferences.NotifyOnNewClubQuestion,
                NotifyOnAnswerPublishedOnMyQuestion = preferences.NotifyOnAnswerPublishedOnMyQuestion,
                NotifyOnAnswerToMyQuestionMarkedOfficial = preferences.NotifyOnAnswerToMyQuestionMarkedOfficial,
                NotifyOnMyQuestionOrAnswerModifiedByAdmin = preferences.NotifyOnMyQuestionOrAnswerModifiedByAdmin,
                NotifyOnAnyOfficialAnswerInQuestionIrelated = preferences.NotifyOnAnyOfficialAnswerInQuestionIrelated,
                NotifyOnQuestionIrelatedModifiedByAuthor = preferences.NotifyOnQuestionIrelatedModifiedByAuthor,
                NotifyOnNewAnswerInQuestionIrelated = preferences.NotifyOnNewAnswerInQuestionIrelated
            };
        
            return await userRepository.UpdateNotificationPreferencesAsync(updatedPreferences);
        }
        #endregion
    }
}