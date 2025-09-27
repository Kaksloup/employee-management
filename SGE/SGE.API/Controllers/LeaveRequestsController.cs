using Microsoft.AspNetCore.Mvc;
using SGE.Application.DTOs.LeaveRequests;
using SGE.Application.Interfaces.Services;
using SGE.Core.Enums;

namespace SGE.API.Controllers;

/// <summary>
/// Controller responsible for handling leave request-related operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LeaveRequestsController(ILeaveRequestService leaveRequestService)  : ControllerBase
{
    /// <summary>
    /// Creates a new leave request.
    /// </summary>
    /// <param name="dto">The data transfer object containing details of the leave request to create.</param>
    /// <returns>The created leave request details.</returns>
    [HttpPost]
    public async Task<ActionResult<LeaveRequestDto>> Create([FromBody] LeaveRequestCreateDto dto)
    {
        try
        {
            var result = await leaveRequestService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Retrieves a leave request by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the leave request to retrieve.</param>
    /// <returns>The leave request details if found; otherwise, a NotFound result.</returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<LeaveRequestDto>> GetById(int id)
    {
        var leave = await leaveRequestService.GetByIdAsync(id);
        if (leave == null)
            return NotFound();

        return Ok(leave);
    }

    /// <summary>
    /// Retrieves all leave requests for a specific employee.
    /// </summary>
    /// <param name="employeeId">The unique identifier of the employee whose leave requests are to be retrieved.</param>
    /// <returns>A collection of leave requests associated with the specified employee.</returns>
    [HttpGet("employee/{employeeId:int}")]
    public async Task<ActionResult<IEnumerable<LeaveRequestDto>>> GetByEmployee(int employeeId)
    {
        var result = await leaveRequestService.GetLeaveRequestsByEmployeeAsync(employeeId);
        return Ok(result);
    }

    /// <summary>
    /// Retrieves a collection of leave requests filtered by their status.
    /// </summary>
    /// <param name="status">The status of the leave requests to retrieve (e.g., Pending, Approved, Rejected, or Cancelled).</param>
    /// <returns>A collection of leave request details that match the specified status.</returns>
    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<LeaveRequestDto>>> GetByStatus(LeaveStatus status)
    {
        var result = await leaveRequestService.GetLeaveRequestsByStatusAsync(status);
        return Ok(result);
    }

    /// <summary>
    /// Retrieves all leave requests that are currently pending approval.
    /// </summary>
    /// <returns>A collection of leave requests in a pending status.</returns>
    [HttpGet("pending")]
    public async Task<ActionResult<IEnumerable<LeaveRequestDto>>> GetPending()
    {
        var result = await leaveRequestService.GetPendingLeaveRequestsAsync();
        return Ok(result);
    }

    /// <summary>
    /// Updates the status of an existing leave request.
    /// </summary>
    /// <param name="id">The unique identifier of the leave request to update.</param>
    /// <param name="dto">The data transfer object containing the updated status and optional manager comments for the leave request.</param>
    /// <returns>An ActionResult indicating the outcome of the update operation.</returns>
    [HttpPut("{id:int}/status")]
    public async Task<ActionResult> UpdateStatus(int id, [FromBody] LeaveRequestUpdateDto dto)
    {
        var success = await leaveRequestService.UpdateStatusAsync(id, dto);
        if (!success)
            return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Retrieves the remaining leave days for a specific employee for a given year.
    /// </summary>
    /// <param name="employeeId">The unique identifier of the employee.</param>
    /// <param name="year">The year for which the remaining leave days are to be retrieved.</param>
    /// <returns>The number of remaining leave days for the specified employee and year.</returns>
    [HttpGet("employee/{employeeId:int}/remaining/{year:int}")]
    public async Task<ActionResult<int>> GetRemainingLeaveDays(int employeeId, int year)
    {
        var result = await leaveRequestService.GetRemainingLeaveDaysAsync(employeeId, year);
        return Ok(result);
    }

    /// <summary>
    /// Checks if there is a conflicting leave request for the specified employee within the given date range.
    /// </summary>
    /// <param name="employeeId">The unique identifier of the employee whose leave requests are to be checked for conflicts.</param>
    /// <param name="startDate">The start date of the leave request being checked.</param>
    /// <param name="endDate">The end date of the leave request being checked.</param>
    /// <param name="excludeRequestId">An optional parameter specifying the ID of a leave request to exclude from the conflict check (if applicable).</param>
    /// <returns>A boolean indicating whether there is a conflict with an existing leave request.</returns>
    [HttpGet("conflict-check")]
    public async Task<ActionResult<bool>> HasConflict(
        [FromQuery] int employeeId,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate,
        [FromQuery] int? excludeRequestId = null)
    {
        var hasConflict = await leaveRequestService.HasConflictingLeaveAsync(employeeId, startDate, endDate, excludeRequestId);
        return Ok(hasConflict);
    }
}