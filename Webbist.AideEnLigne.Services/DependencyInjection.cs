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

using Microsoft.Extensions.DependencyInjection;
using Webbist.AideEnLigne.Services.Answers;
using Webbist.AideEnLigne.Services.AnswerVotes;
using Webbist.AideEnLigne.Services.Notifications;
using Webbist.AideEnLigne.Services.Questions;
using Webbist.AideEnLigne.Services.Users;

namespace Webbist.AideEnLigne.Services
{
    /// <summary>
    /// Represents the dependency injection configuration for the services layer.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds application services to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The application configuration.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<Notification>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<IAnswerVoteService, AnswerVoteService>();

            return services;
        }
    }
}