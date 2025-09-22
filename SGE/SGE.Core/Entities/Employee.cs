namespace SGE.Core.Entities;

public class Employee : BaseEntity
{
    /// <summary>
    /// Gets or sets the first name of the employee.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name of the employee.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address of the employee.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the phone number associated with the employee.
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
    /// Gets or sets the date the employee was hired.
    /// </summary>
    public DateTime HireDate { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the department associated with the employee.
    /// </summary>
    public int DepartmentId { get; set; }

    /// <summary>
    /// Gets or sets the department associated with the employee.
    /// </summary>
    public virtual Department Department { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of attendance records associated with the employee.
    /// </summary>
    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    /// <summary>
    /// Gets or sets the collection of leave requests associated with the employee.
    /// </summary>
    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();

}