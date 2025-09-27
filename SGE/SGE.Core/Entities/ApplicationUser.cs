using Microsoft.AspNetCore.Identity;

namespace SGE.Core.Entities;

public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the timestamp indicating when the entity was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the date and time of the user's last login.
    /// </summary>
    public DateTime? LastLoginAt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user account is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the identifier for the associated employee.
    /// </summary>
    public int? EmployeeId { get; set; }

    /// <summary>
    /// Represents an employee within the system, including personal details, position,
    /// salary information, and relationships with departments, attendance, and leave requests.
    /// </summary>
    public Employee? Employee { get; set; }

    /// <summary>
    /// Gets or sets the collection of refresh tokens associated with the user.
    /// </summary>
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
