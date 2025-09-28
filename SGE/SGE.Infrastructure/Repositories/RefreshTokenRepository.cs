using Microsoft.EntityFrameworkCore;
using SGE.Application.Interfaces.Repositories;
using SGE.Core.Entities;
using SGE.Infrastructure.Data;

namespace SGE.Infrastructure.Repositories;

/// <summary>
/// Provides access to RefreshToken data operations, implementing common repository
/// patterns and custom functionality specific to RefreshTokens.
/// </summary>
public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
{
    /// <summary>
    /// Represents a repository that provides data access methods for handling
    /// operations related to the RefreshToken entity within the underlying
    /// database. Inherits common repository functionality and defines additional
    /// methods to specifically manage RefreshTokens.
    /// </summary>
    public RefreshTokenRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Retrieves a refresh token entity by its associated token string from the database.
    /// </summary>
    /// <param name="token">The token value used to identify the refresh token.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains
    /// the matching <see cref="RefreshToken"/> if found, or null if no match exists.</returns>
    public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(rt => rt.Token == token, cancellationToken);
    }

    /// <summary>
    /// Retrieves all active refresh tokens associated with a specific user.
    /// Active tokens are those that have not expired and have not been revoked.
    /// </summary>
    /// <param name="userId">The identifier of the user whose active refresh tokens are to be retrieved.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of active refresh tokens belonging to the specified user.</returns>
    public async Task<IEnumerable<RefreshToken>> GetActiveTokensByUserAsync(string userId,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(rt => rt.UserId == userId && rt.RevokedAt == null).ToListAsync(cancellationToken);
    }
}