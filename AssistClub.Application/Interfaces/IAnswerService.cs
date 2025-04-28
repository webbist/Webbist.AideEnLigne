using AssistClub.Application.DTOs;
using Domain.Enums;

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
    /// <param name="userId">The ID of the user to check if he voted for any answer.</param>
    /// <returns>
    /// An <see cref="IQueryable{T}"/> of <see cref="AnswerResponse"/> containing the answers.
    /// </returns>
    Task<IQueryable<AnswerResponse>> GetAnswersAsync(Guid userId);
    
    /// <summary>
    /// Updates the status of an answer and the associated question status.
    /// </summary>
    /// <param name="id">The unique identifier of the answer to be updated.</param>
    /// <param name="newStatus">The new status to be set for the answer.</param>
    /// <returns>
    /// Returns <c>true</c> if the update was successful; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> UpdateAnswerStatusAsync(Guid id, AnswerStatus newStatus);
    
    /// <summary>
    /// Updates an existing answer in the database.
    /// </summary>
    /// <param name="id">The unique identifier of the answer to be updated.</param>
    /// <param name="answerRequest">The <see cref="AnswerRequest"/> containing the updated answer details.</param>
    /// <returns>
    /// A boolean indicating whether the update was successful (<c>true</c> if successful, <c>false</c> otherwise).
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if the answer content exceeds <see cref="AnswerRequest.ContentMaxLength"/> characters.
    /// </exception>
    Task<bool> UpdateAnswerAsync(Guid id, AnswerRequest answerRequest);

    /// <summary>
    /// Deletes an answer from the system.
    /// </summary>
    /// <param name="answerId">The ID of the answer to be deleted.</param>
    /// <returns>
    /// A boolean indicating whether the deletion was successful (<c>true</c> if successful, <c>false</c> otherwise).
    /// </returns>
    Task<bool> DeleteAnswerAsync(Guid answerId);
}