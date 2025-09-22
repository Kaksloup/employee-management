using SGE.Core.Enums;

namespace SGE.Core.Entities;

public class LeaveRequest : BaseEntity
{
    /// <summary>
    /// Represents the type of leave being requested, such as vacation, sick leave,
    /// maternity leave, or other types defined by the organization.
    /// </summary>
    public LeaveType LeaveType { get; set; }

    /// <summary>
    /// Represents the starting date of the leave request.
    /// Indicates the first day the employee intends to take leave.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Represents the date on which the leave ends. This property is intended
    /// to define the final day of the leave period for the leave request.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Represents the total number of days being requested for leave in the leave request.
    /// This value is typically calculated based on the difference between the start date
    /// and end date of the leave period.
    /// </summary>
    public int DaysRequested { get; set; }

    /// <summary>
    /// Specifies the reason provided by the employee for requesting leave.
    /// It typically includes details or justification for the leave request.
    /// </summary>
    public string Reason { get; set; } = string.Empty;

    /// <summary>
    /// Indicates the current status of the leave request. It can represent various
    /// statuses such as pending, approved, or rejected.
    /// </summary>
    public LeaveStatus Status { get; set; } = LeaveStatus.Pending;

    /// <summary>
    /// Contains any comments or feedback provided by the manager regarding the leave request,
    /// such as approval justification, concerns, or reasons for rejection.
    /// </summary>
    public string? ManagerComments { get; set; }

    /// <summary>
    /// Indicates the date and time when the leave request was reviewed by a manager or authorized individual.
    /// This property is set once the review process is completed.
    /// </summary>
    public DateTime? ReviewedAt { get; set; }

    /// <summary>
    /// Identifies the individual who reviewed the leave request, typically
    /// set to the name or identifier of a manager or an authorized reviewer.
    /// </summary>
    public string? ReviewedBy { get; set; }

    /// <summary>
    /// Represents the unique identifier of the employee associated with the current leave request.
    /// This property establishes a relationship between the leave request and a specific employee.
    /// </summary>
    public int EmployeeId { get; set; }

    /// <summary>
    /// Represents an employee with detailed information such as
    /// personal details, contact information, position, salary,
    /// hire details, and relationships with other entities like
    /// attendance records, department, and leave requests.
    /// </summary>
    public virtual Employee Employee { get; set; } = null!;

}