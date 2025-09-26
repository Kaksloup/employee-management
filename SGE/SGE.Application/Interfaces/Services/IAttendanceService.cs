using SGE.Application.DTOs.Attendances;

namespace SGE.Application.Interfaces.Services;

/// <summary>
/// Defines the contract for attendance management services.
/// Provides methods for employees to clock in and out, retrieve attendance data,
/// and calculate work hours.
/// </summary>
public interface IAttendanceService
{
    /// <summary>
    /// Records the clock-in time for an employee.
    /// </summary>
    /// <param name="clockInDto">An object containing clock-in information such as employee ID, timestamp, and notes.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the attendance details after clock-in.</returns>
    Task<AttendanceDto> ClockInAsync(ClockInOutDto clockInDto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Records the clock-out time for an employee.
    /// </summary>
    /// <param name="clockOutDto">An object containing clock-out information such as employee ID, timestamp, and notes.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the attendance details after clock-out.</returns>
    Task<AttendanceDto> ClockOutAsync(ClockInOutDto clockOutDto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new attendance record for an employee.
    /// </summary>
    /// <param name="createAttendanceDto">An object containing the details for the attendance record such as employee ID, date, clock-in time, clock-out time, break duration, and notes.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the details of the created attendance record.</returns>
    Task<AttendanceDto> CreateAttendanceAsync(AttendanceCreateDto createAttendanceDto,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the attendance record for a specific ID.
    /// </summary>
    /// <param name="id">The unique identifier of the attendance record.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the attendance details for the specified ID, or null if no record is found.</returns>
    Task<AttendanceDto?> GetAttendanceByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves attendance records for a specific employee within a specified date range.
    /// </summary>
    /// <param name="employeeId">The unique identifier of the employee whose attendance records are to be retrieved.</param>
    /// <param name="startDate">The beginning of the date range for the attendance query. If null, no start date filter is applied.</param>
    /// <param name="endDate">The end of the date range for the attendance query. If null, no end date filter is applied.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of attendance records matching the query parameters.</returns>
    Task<IEnumerable<AttendanceDto>> GetAttendancesByEmployeeAsync(int employeeId, DateTime? startDate = null,
        DateTime? endDate = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a collection of attendance records for all employees for a specific date.
    /// </summary>
    /// <param name="date">The date for which to retrieve attendance records.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of attendance details for the specified date.</returns>
    Task<IEnumerable<AttendanceDto>> GetAttendancesByDateAsync(DateTime date,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the attendance details for an employee for the current day.
    /// </summary>
    /// <param name="employeeId">The unique identifier of the employee whose attendance is to be retrieved.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the attendance details for the current day, or null if no attendance record exists.</returns>
    Task<AttendanceDto?> GetTodayAttendanceAsync(int employeeId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Calculates the total hours worked by an employee during a specific month and year.
    /// </summary>
    /// <param name="employeeId">The ID of the employee whose work hours are to be calculated.</param>
    /// <param name="year">The year for which the work hours should be calculated.</param>
    /// <param name="month">The month for which the work hours should be calculated.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the total worked hours as a decimal value.</returns>
    Task<decimal> GetMonthlyWorkedHoursAsync(int employeeId, int year, int month,
        CancellationToken cancellationToken = default);
}