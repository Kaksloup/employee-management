namespace SGE.Application.DTOs.Users;

public class RefreshTokenDto
{
    /// <summary>
    /// Gets or sets the access token used for authentication and authorization purposes.
    /// This token is typically a short-lived JWT (JSON Web Token) issued to the user.
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the refresh token used to obtain a new access token once the current token expires.
    /// This token is generally long-lived and is used to maintain user sessions without requiring re-authentication.
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;

}