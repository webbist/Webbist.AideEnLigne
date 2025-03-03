using AssistClub.Application.Interfaces;
using AssistClub.Application.Services;
using AssistClub.Infrastructure.Persistence;
using AssistClub.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AssistClub.Infrastructure;

/// <summary>
/// Dependency injection for the infrastructure layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds infrastructure services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AssistClubDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        return services;
    }
}