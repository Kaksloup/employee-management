namespace SGE.Application.DTOs.Employees;

public class EmployeeUpdateDto
{
    /// <summary>
    /// Gets or sets the phone number of the employee.
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the address of the employee.
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the position or job title of the employee.
    /// </summary>
    public string Position { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the salary of the employee.
    /// </summary>
    public decimal Salary { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the department associated with the employee.
    /// </summary>
    public int DepartmentId { get; set; }
}