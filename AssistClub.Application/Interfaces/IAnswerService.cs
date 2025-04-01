using AssistClub.Application.DTOs;

namespace AssistClub.Application.Interfaces;

/// <summary>
/// Represents the service interface for handling business logic related to answers.
/// </summary>
/// <remarks>
/// Ensures that answers are validated, processed, and formatted
/// correctly before being stored in the database or returned to the client.
/// Acts as a bridge between the repository and the API layer.
/// </remarks>
public interface IAnswerService
{
    /// <summary>
    /// Creates a new answer in the system.
    /// </summary>
    /// <remarks>
    /// Validates the input and delegates the persistence to the repository.
    /// Returns a simplified view model for external use.
    /// </remarks>
    /// <param name="answerRequest">The <see cref="AnswerRequest"/> containing answer details.</param>
    /// <returns>A <see cref="AnswerResponse"/> representing the created answer.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown if the answer content exceeds <see cref="AnswerRequest.ContentMaxLength"/> characters.
    /// </exception>
    Task<AnswerResponse> CreateAnswerAsync(AnswerRequest answerRequest);
}