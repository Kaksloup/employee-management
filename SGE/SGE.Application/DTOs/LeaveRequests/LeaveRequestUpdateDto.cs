using SGE.Core.Enums;

namespace SGE.Application.DTOs.LeaveRequests;

public class LeaveRequestUpdateDto
{
    /// <summary>
    /// Gets or sets the status of the leave request.
    /// </summary>
    /// <remarks>
    /// The status indicates the current state of the leave request, such as Pending, Approved, Rejected, or Cancelled.
    /// </remarks>
    public LeaveStatus Status { get; set; } // Approved, Rejected, Cancelled

    /// <summary>
    /// Gets or sets the comments provided by the manager regarding the leave request.
    /// </summary>
    /// <remarks>
    /// These comments may include feedback, reasons for approval or rejection, or any additional notes relevant to the leave request.
    /// </remarks>
    public string? ManagerComments { get; set; }

}