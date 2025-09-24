namespace SGE.Application.DTOs.Employees;

public class EmployeeDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the employee.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the full name of the employee.
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address of the employee.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the position or job title of the employee.
    /// </summary>
    public string Position { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the salary of the employee.
    /// </summary>
    public decimal Salary { get; set; }

    /// <summary>
    /// Gets or sets the name of the department associated with the employee.
    /// </summary>
    public string DepartmentName { get; set; } = string.Empty;
}