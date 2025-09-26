namespace SGE.Application.DTOs.Attendances;

public class ClockInOutDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the employee associated with the clock-in or clock-out entry.
    /// </summary>
    public int EmployeeId { get; set; }

    /// <summary>
    /// Gets or sets the date and time for the clock-in or clock-out entry.
    /// </summary>
    public DateTime DateTime { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets additional information or remarks associated with the clock-in or clock-out entry.
    /// </summary>
    public string Notes { get; set; } = string.Empty;

}