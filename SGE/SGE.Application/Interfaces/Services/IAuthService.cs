using SGE.Application.DTOs.Users;

namespace SGE.Application.Interfaces.Services;

/// <summary>
/// Interface responsible for managing authentication and authorization operations.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Registers a new user with the provided registration details.
    /// </summary>
    /// <param name="registerDto">An object containing the user's registration details such as first name, last name, email, username, password, and optional employee ID.</param>
    /// <returns>A Task that represents the asynchronous operation. When completed successfully, the task returns an <see cref="AuthResponseDto"/> containing authentication details including access token, refresh token, expiration details, and user information.</returns>
    Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);

    /// <summary>
    /// Authenticates a user based on the provided login details.
    /// </summary>
    /// <param name="loginDto">An object containing the user's login credentials, including email and password.</param>
    /// <returns>A Task that represents the asynchronous operation. When completed successfully, the task returns an <see cref="AuthResponseDto"/> containing authentication details such as access token, refresh token, expiration details, and user information.</returns>
    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);

    /// <summary>
    /// Refreshes the authentication tokens using the provided refresh token details.
    /// </summary>
    /// <param name="refreshTokenDto">An object containing the current access token and refresh token.</param>
    /// <returns>A Task that represents the asynchronous operation. When completed successfully, the task returns an <see cref="AuthResponseDto"/> containing updated authentication details including a new access token, refresh token, expiration details, and user information.</returns>
    Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);

    /// <summary>
    /// Logs out a user by invalidating their current session or authentication tokens.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to log out.</param>
    /// <returns>A Task that represents the asynchronous operation. When completed successfully, the task returns a boolean value indicating whether the user was successfully logged out.</returns>
    Task<bool> LogoutAsync(string userId);

    /// <summary>
    /// Revokes an active token to terminate its access and prevent further usage.
    /// </summary>
    /// <param name="token">The token that needs to be revoked for invalidation.</param>
    /// <returns>A Task that represents the asynchronous operation. When completed, the task returns a boolean indicating whether the token was successfully revoked.</returns>
    Task<bool> RevokeTokenAsync(string token);

    /// <summary>
    /// Retrieves the current user's information based on the provided user ID.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose information is to be retrieved.</param>
    /// <returns>A Task that represents the asynchronous operation. When completed successfully, the task returns a <see cref="UserDto"/> containing details about the user, including their ID, username, email, name, roles, and optional employee ID. Returns null if the user does not exist.</returns>
    Task<UserDto?> GetCurrentUserAsync(string userId);
}