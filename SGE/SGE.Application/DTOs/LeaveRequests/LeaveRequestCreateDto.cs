using SGE.Core.Enums;

namespace SGE.Application.DTOs.LeaveRequests;

public class LeaveRequestCreateDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the employee associated with the leave request.
    /// </summary>
    public int EmployeeId { get; set; }

    /// <summary>
    /// Gets or sets the type of leave requested, represented as an enumeration (e.g., Annual, Sick, etc.).
    /// </summary>
    public LeaveType LeaveType { get; set; } // Enum : Annual, Sick, etc.

    /// <summary>
    /// Gets or sets the start date of the leave request.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the requested leave period.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Gets or sets the reason provided by the employee for the leave request.
    /// </summary>
    public string Reason { get; set; } = string.Empty;

}