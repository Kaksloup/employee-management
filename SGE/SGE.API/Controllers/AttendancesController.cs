using Microsoft.AspNetCore.Mvc;
using SGE.Application.DTOs.Attendances;
using SGE.Application.Interfaces.Services;

namespace SGE.API.Controllers;

/// <summary>
/// Controller for managing employee attendance data.
/// Provides endpoints for clocking in, clocking out, retrieving attendance records, and calculating attendance-related data.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AttendancesController(IAttendanceService attendanceService) : ControllerBase
{
    /// <summary>
    /// Records the clock-in time for an employee.
    /// </summary>
    /// <param name="clockInDto">The data transfer object containing clock-in details.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>Returns the updated attendance details of the employee.</returns>
    [HttpPost("clock-in")]
    [ProducesResponseType(200, Type = typeof(AttendanceDto))]
    [ProducesResponseType(400)]
    public async Task<ActionResult<AttendanceDto>> ClockIn([FromBody] ClockInOutDto clockInDto, CancellationToken cancellationToken)
    {
        try
        {
            var attendance = await attendanceService.ClockInAsync(clockInDto, cancellationToken);
            return Ok(attendance);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


    /// <summary>
    /// Records the clock-out time for an employee.
    /// </summary>
    /// <param name="clockOutDto">The data transfer object containing clock-out details.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>Returns the updated attendance details of the employee.</returns>
    [HttpPost("clock-out")]
    [ProducesResponseType(200, Type = typeof(AttendanceDto))]
    [ProducesResponseType(400)]
    public async Task<ActionResult<AttendanceDto>> ClockOut([FromBody] ClockInOutDto clockOutDto, CancellationToken cancellationToken)
    {
        try
        {
            var attendance = await attendanceService.ClockOutAsync(clockOutDto, cancellationToken);
            return Ok(attendance);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new attendance record.
    /// </summary>
    /// <param name="createAttendanceDto">The data transfer object containing the details for the new attendance record.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation, if required.</param>
    /// <returns>Returns the created attendance details.</returns>
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(AttendanceDto))]
    [ProducesResponseType(400)]
    public async Task<ActionResult<AttendanceDto>> CreateAttendance([FromBody] AttendanceCreateDto createAttendanceDto, CancellationToken cancellationToken)
    {
        try
        {
            var attendance = await attendanceService.CreateAttendanceAsync(createAttendanceDto, cancellationToken);
            return CreatedAtAction(nameof(GetAttendance), new { id = attendance.Id }, attendance);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves the attendance record for a specific ID.
    /// </summary>
    /// <param name="id">The unique identifier of the attendance record to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>Returns the attendance details if found; otherwise, a not found response.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(AttendanceDto))]
    [ProducesResponseType(404)]
    public async Task<ActionResult<AttendanceDto>> GetAttendance(int id, CancellationToken cancellationToken)
    {
        try
        {
            var attendance = await attendanceService.GetAttendanceByIdAsync(id, cancellationToken);
            if (attendance == null)
                return NotFound($"Attendance record with ID {id} not found");

            return Ok(attendance);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves the attendance records of a specific employee within an optional date range.
    /// </summary>
    /// <param name="employeeId">The unique identifier of the employee.</param>
    /// <param name="startDate">The start date of the attendance query (optional).</param>
    /// <param name="endDate">The end date of the attendance query (optional).</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>Returns a collection of attendance records for the specified employee and date range.</returns>
    [HttpGet("employee/{employeeId:int}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AttendanceDto>))]
    public async Task<ActionResult<IEnumerable<AttendanceDto>>> GetEmployeeAttendances(
        int employeeId,
        [FromQuery] DateTime? startDate = null, 
        [FromQuery] DateTime? endDate = null, CancellationToken cancellationToken = default)
    {
        try
        {
            var attendances = await attendanceService.GetAttendancesByEmployeeAsync(employeeId, startDate, endDate, cancellationToken);
            return Ok(attendances);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a list of attendance records for a specific date.
    /// </summary>
    /// <param name="date">The date for which to retrieve attendance records.</param>
    /// <param name="cancellationToken">Token used to signal cancellation of the operation.</param>
    /// <returns>Returns a collection of attendance records for the specified date.</returns>
    [HttpGet("date/{date:datetime}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AttendanceDto>))]
    public async Task<ActionResult<IEnumerable<AttendanceDto>>> GetAttendancesByDate(DateTime date, CancellationToken cancellationToken)
    {
        try
        {
            var attendances = await attendanceService.GetAttendancesByDateAsync(date, cancellationToken);
            return Ok(attendances);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves today's attendance record for a specific employee.
    /// </summary>
    /// <param name="employeeId">The unique identifier of the employee whose attendance is being retrieved.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation if needed.</param>
    /// <returns>Returns the attendance details for the employee for the current day.</returns>
    [HttpGet("employee/{employeeId:int}/today")]
    [ProducesResponseType(200, Type = typeof(AttendanceDto))]
    [ProducesResponseType(404)]
    public async Task<ActionResult<AttendanceDto>> GetTodayAttendance(int employeeId, CancellationToken cancellationToken)
    {
        try
        {
            var attendance = await attendanceService.GetTodayAttendanceAsync(employeeId, cancellationToken);
            if (attendance == null)
                return NotFound("No attendance record found for today");

            return Ok(attendance);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves the total number of hours worked by an employee for a specific month and year.
    /// </summary>
    /// <param name="employeeId">The unique identifier of the employee.</param>
    /// <param name="year">The year for which the hours are being calculated.</param>
    /// <param name="month">The month for which the hours are being calculated.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>Returns the total hours worked by the employee as a decimal value.</returns>
    [HttpGet("employee/{employeeId:int}/hours/{year:int}/{month:int}")]
    [ProducesResponseType(200, Type = typeof(decimal))]
    public async Task<ActionResult<decimal>> GetMonthlyHours(int employeeId, int year, int month, CancellationToken cancellationToken)
    {
        try
        {
            var totalHours = await attendanceService.GetMonthlyWorkedHoursAsync(employeeId, year, month, cancellationToken);
            return Ok(totalHours);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}