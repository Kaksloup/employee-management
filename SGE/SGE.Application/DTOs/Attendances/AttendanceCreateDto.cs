namespace SGE.Application.DTOs.Attendances;

public class AttendanceCreateDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the employee associated with the attendance record.
    /// </summary>
    public int EmployeeId { get; set; }

    /// <summary>
    /// Gets or sets the date associated with the attendance record.
    /// </summary>
    public DateTime Date { get; set; } = DateTime.Today;

    /// <summary>
    /// Gets or sets the optional clock-in time for the attendance record.
    /// </summary>
    public TimeSpan? ClockIn { get; set; }

    /// <summary>
    /// Gets or sets the time the employee clocked out for the attendance record.
    /// </summary>
    public TimeSpan? ClockOut { get; set; }

    /// <summary>
    /// Gets or sets the total duration of break time in hours during the workday.
    /// </summary>
    public double BreakDurationHours { get; set; } = 0;

    /// <summary>
    /// Gets or sets additional information or comments related to the attendance record.
    /// </summary>
    public string Notes { get; set; } = string.Empty;
}