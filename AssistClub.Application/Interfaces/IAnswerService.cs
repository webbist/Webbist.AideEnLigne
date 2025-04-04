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
    
    /// <summary>
    /// Retrieves all answers in the system, including user information.
    /// </summary>
    /// <returns>
    /// An <see cref="IQueryable{T}"/> of <see cref="AnswerResponse"/> containing the answers.
    /// </returns>
    Task<IQueryable<AnswerResponse>> GetAnswersAsync();
    
    /// <summary>
    /// Updates the official status of an answer and the associated question status.
    /// </summary>
    /// <param name="id">The unique identifier of the answer to be updated.</param>
    /// <param name="isOfficial">Indicates whether the answer is official or not.</param>
    /// <returns>
    /// Returns <c>true</c> if the update was successful; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> UpdateAnswerOfficialStatusAsync(Guid id, bool isOfficial);
}