using SGE.Core.Enums;

namespace SGE.Application.DTOs.LeaveRequests;

public class LeaveRequestDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the leave request.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the employee related to the leave request.
    /// </summary>
    public int EmployeeId { get; set; }

    /// <summary>
    /// Gets or sets the name of the employee associated with the leave request.
    /// </summary>
    public string EmployeeName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of leave being requested, represented by the LeaveType enumeration.
    /// </summary>
    public LeaveType LeaveType { get; set; }

    /// <summary>
    /// Gets or sets the textual representation of the leave type associated with the leave request.
    /// </summary>
    public string LeaveTypeName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the start date of the leave request.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the leave request.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Gets or sets the total number of days requested for the leave.
    /// </summary>
    public int DaysRequested { get; set; }

    /// <summary>
    /// Gets or sets the reason provided by the employee for requesting leave.
    /// </summary>
    public string Reason { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the status of the leave request.
    /// </summary>
    public LeaveStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the name representation of the leave request's current status.
    /// </summary>
    public string StatusName { get; set; } = string.Empty;

}