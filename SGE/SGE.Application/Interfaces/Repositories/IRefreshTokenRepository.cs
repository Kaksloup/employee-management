using SGE.Core.Entities;

namespace SGE.Application.Interfaces.Repositories;

/// <summary>
/// Defines the contract for a repository handling operations related to refresh tokens.
/// </summary>
public interface IRefreshTokenRepository: IRepository<RefreshToken>
{
    /// <summary>
    /// Retrieves a refresh token by its token value asynchronously.
    /// </summary>
    /// <param name="token">The token value of the refresh token to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, containing the refresh token if found; otherwise, null.</returns>
    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all active refresh tokens associated with a specific user asynchronously.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose active refresh tokens are to be retrieved.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, containing a collection of active refresh tokens for the specified user.</returns>
    Task<IEnumerable<RefreshToken>> GetActiveTokensByUserAsync(string userId,
        CancellationToken cancellationToken = default);
}