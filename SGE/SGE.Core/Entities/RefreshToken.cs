namespace SGE.Core.Entities;

public class RefreshToken
{
    /// <summary>
    /// Represents the unique identifier for the refresh token.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Represents the token string associated with the refresh token.
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Specifies the date and time when the refresh token will expire.
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Represents the date and time when the refresh token was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Represents the date and time when the refresh token was revoked.
    /// </summary>
    public DateTime? RevokedAt { get; set; }

    /// <summary>
    /// Represents the token that replaced this refresh token, if applicable.
    /// </summary>
    public string? ReplacedByToken { get; set; }

    /// <summary>
    /// Specifies the reason for which the refresh token was revoked.
    /// </summary>
    public string? ReasonRevoked { get; set; }

    /// <summary>
    /// Represents the identifier of the user associated with the refresh token.
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Represents the user associated with the refresh token.
    /// </summary>
    public ApplicationUser User { get; set; } = null!;

    /// <summary>
    /// Indicates whether the refresh token has expired based on the current UTC time and the expiration date.
    /// </summary>
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

    /// <summary>
    /// Indicates whether the refresh token has been revoked.
    /// </summary>
    public bool IsRevoked => RevokedAt != null;

    /// <summary>
    /// Indicates whether the refresh token is currently active.
    /// A token is considered active if it is not revoked and has not expired.
    /// </summary>
    public bool IsActive => !IsRevoked && !IsExpired;

}