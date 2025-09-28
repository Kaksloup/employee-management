using System.Security.Claims;
using SGE.Core.Entities;

namespace SGE.Application.Interfaces.Services;

public interface ITokenService
{
    /// <summary>
    /// Generates a JWT access token for a given user based on their roles.
    /// </summary>
    /// <param name="user">The user for whom the access token is to be generated.</param>
    /// <param name="roles">The list of roles associated with the user.</param>
    /// <returns>The generated JWT access token as a string.</returns>
    string GenerateAccessToken(ApplicationUser user, IList<string> roles);

    /// <summary>
    /// Generates a new refresh token for user authentication and session management.
    /// </summary>
    /// <returns>The generated refresh token as a string.</returns>
    string GenerateRefreshToken();

    /// <summary>
    /// Extracts the claims principal from an expired JWT token without validating the token's expiration.
    /// </summary>
    /// <param name="token">The expired JWT token from which to extract the claims principal.</param>
    /// <returns>The claims principal extracted from the expired token.</returns>
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

    /// <summary>
    /// Creates a refresh token asynchronously for user authentication and session management.
    /// </summary>
    /// <param name="userId">The unique identifier of the user for whom the refresh token is being created.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created refresh token.</returns>
    Task<RefreshToken> CreateRefreshTokenAsync(string userId);

    /// <summary>
    /// Validates the provided refresh token for the specified user asynchronously.
    /// </summary>
    /// <param name="token">The refresh token to validate.</param>
    /// <param name="userId">The unique identifier of the user associated with the refresh token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result indicates whether the refresh token is valid.</returns>
    Task<bool> ValidateRefreshTokenAsync(string token, string userId);

    /// <summary>
    /// Revokes a refresh token asynchronously for a specified reason.
    /// </summary>
    /// <param name="token">The refresh token to be revoked.</param>
    /// <param name="reason">The reason for revoking the refresh token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result indicates whether the revocation was successful.</returns>
    Task RevokeRefreshTokenAsync(string token, string reason);

    /// <summary>
    /// Revokes all active refresh tokens associated with a specific user asynchronously.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose refresh tokens are to be revoked.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task RevokeAllUserRefreshTokensAsync(string userId);
}