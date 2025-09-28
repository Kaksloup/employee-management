namespace SGE.Application.DTOs.Users;

public class LoginDto
{
    /// <summary>
    /// Represents the email address of the user.
    /// This property is used as a credential for login operations.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Represents the password of the user.
    /// This property is used as a credential for login operations.
    /// </summary>
    public string Password { get; set; } = string.Empty;
    
}