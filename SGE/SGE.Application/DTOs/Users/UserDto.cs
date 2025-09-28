namespace SGE.Application.DTOs.Users;

public class UserDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address associated with the user.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of roles assigned to the user.
    /// </summary>
    public IList<string> Roles { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the identifier associated with the employee.
    /// </summary>
    public int? EmployeeId { get; set; }
}