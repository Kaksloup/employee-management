using SGE.Application.DTOs.LeaveRequests;
using SGE.Core.Enums;

namespace SGE.Application.Interfaces.Services;

public interface ILeaveRequestService
{
    /// <summary>
    /// Creates a new leave request based on the provided data.
    /// </summary>
    /// <param name="dto">The details of the leave request to be created, including employee ID, leave type, start date, end date, and reason.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created leave request details as a <c>LeaveRequestDto</c>.</returns>
    Task<LeaveRequestDto> CreateAsync(LeaveRequestCreateDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the details of a leave request by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the leave request to be retrieved.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the leave request details as a <c>LeaveRequestDto</c>, or <c>null</c> if the leave request is not found.</returns>
    Task<LeaveRequestDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all leave requests submitted by a specific employee.
    /// </summary>
    /// <param name="employeeId">The unique identifier of the employee whose leave requests are to be retrieved.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of leave requests submitted by the specified employee as <c>LeaveRequestDto</c> objects.</returns>
    Task<IEnumerable<LeaveRequestDto>> GetLeaveRequestsByEmployeeAsync(int employeeId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves leave requests filtered by the specified status.
    /// </summary>
    /// <param name="status">The status of leave requests to filter by, such as Pending, Approved, Rejected, or Cancelled.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of leave requests with the specified status as <c>LeaveRequestDto</c>.</returns>
    Task<IEnumerable<LeaveRequestDto>> GetLeaveRequestsByStatusAsync(LeaveStatus status,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all leave requests that are currently pending approval.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of pending leave requests represented as <c>LeaveRequestDto</c>.</returns>
    Task<IEnumerable<LeaveRequestDto>> GetPendingLeaveRequestsAsync();

    /// <summary>
    /// Updates the status and other related details of an existing leave request.
    /// </summary>
    /// <param name="id">The unique identifier of the leave request to update.</param>
    /// <param name="dto">The data containing the updated status and optional manager comments.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating whether the update was successful.</returns>
    Task<bool> UpdateStatusAsync(int id, LeaveRequestUpdateDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Calculates the remaining leave days for a specific employee in a given year.
    /// </summary>
    /// <param name="employeeId">The unique identifier of the employee whose remaining leave days are to be calculated.</param>
    /// <param name="year">The year for which the remaining leave days are to be calculated.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the number of remaining leave days as an integer.</returns>
    Task<int> GetRemainingLeaveDaysAsync(int employeeId, int year, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if there are any conflicting leave requests for the specified employee within the given date range.
    /// </summary>
    /// <param name="employeeId">The ID of the employee whose leave requests are being checked for conflicts.</param>
    /// <param name="startDate">The starting date of the leave period to be checked for conflicts.</param>
    /// <param name="endDate">The ending date of the leave period to be checked for conflicts.</param>
    /// <param name="excludeRequestId">An optional leave request ID to exclude from the conflict check.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result indicates whether a conflicting leave request exists (true if a conflict is found, otherwise false).</returns>
    Task<bool> HasConflictingLeaveAsync(int employeeId, DateTime startDate, DateTime endDate,
        int? excludeRequestId = null, CancellationToken cancellationToken = default);
}