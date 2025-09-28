namespace SGE.Application.DTOs.Users;

public class AuthResponseDto
{
    /// <summary>
    /// Represents the access token issued for authentication purposes.
    /// This token is used to grant the user access to protected resources or endpoints.
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// Represents the refresh token issued for maintaining an authenticated session.
    /// This token is used to obtain a new access token after the current access token expires.
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;

    /// <summary>
    /// Indicates the date and time at which the current token will expire.
    /// This property helps determine the validity period of the token and when it should be refreshed.
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Represents the user associated with the authentication response.
    /// Contains details about the user's identity, roles, and personal information.
    /// </summary>
    public UserDto? User { get; set; }

}