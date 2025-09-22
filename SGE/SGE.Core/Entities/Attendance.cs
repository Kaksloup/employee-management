namespace SGE.Core.Entities;

public class Attendance  : BaseEntity
{
    /// <summary>
    /// Represents the specific date associated with an attendance record.
    /// </summary>
    /// <remarks>
    /// This property typically indicates the calendar date for which the attendance entry is recorded.
    /// It does not include time information and serves to distinguish attendance data by day.
    /// </remarks>
    public DateTime Date { get; set; }

    /// <summary>
    /// Represents the clock-in time for an attendance record.
    /// </summary>
    /// <remarks>
    /// This property specifies the time an employee starts their workday or shift.
    /// It is optional and may not be set for attendance entries where a clock-in time is not recorded.
    /// </remarks>
    public TimeSpan? ClockIn { get; set; }

    /// <summary>
    /// Represents the optional clock-out time of an attendance record.
    /// </summary>
    /// <remarks>
    /// This property indicates the time when an employee finishes their work for the day.
    /// It is nullable to accommodate scenarios where an employee has not yet clocked out.
    /// The value typically reflects the local time and does not include date information.
    /// </remarks>
    public TimeSpan? ClockOut { get; set; }

    /// <summary>
    /// Represents the duration of a break taken during a work period.
    /// </summary>
    /// <remarks>
    /// This property records the total time spent on a break within a work session.
    /// The value can be null if no break is recorded.
    /// </remarks>
    public TimeSpan? BreakDuration { get; set; }

    /// <summary>
    /// Represents the total number of hours worked by an employee during a specific attendance entry.
    /// </summary>
    /// <remarks>
    /// This property accounts for the time worked by subtracting break durations
    /// from the time span between clock-in and clock-out entries.
    /// It is expressed in decimal format to allow precise calculations of partial hours.
    /// </remarks>
    public decimal WorkedHours { get; set; }

    /// <summary>
    /// Represents the total hours worked beyond regular working hours on a specific day.
    /// </summary>
    /// <remarks>
    /// This property tracks the additional hours an employee has worked outside of their standard scheduled hours.
    /// It is calculated separately from regular worked hours and may be used for determining overtime pay or compliance with labor regulations.
    /// </remarks>
    public decimal OvertimeHours { get; set; }

    /// <summary>
    /// Represents additional details or comments associated with an attendance entry.
    /// </summary>
    /// <remarks>
    /// This property is used to record supplementary information or context related to a specific attendance record.
    /// It may include notes about deviations, special circumstances, or other remarks relevant to the entry.
    /// </remarks>
    public string Notes { get; set; } = string.Empty;

    /// <summary>
    /// Represents the identifier of the employee associated with a specific attendance record.
    /// </summary>
    /// <remarks>
    /// This property uniquely links an attendance entry to a particular employee.
    /// It acts as a foreign key in the relationship between the Attendance and Employee entities.
    /// </remarks>
    public int EmployeeId { get; set; }

    /// <summary>
    /// Represents an employee within the organization.
    /// </summary>
    /// <remarks>
    /// This property establishes the relationship between an attendance record and its associated employee.
    /// It serves to identify the employee for whom the attendance data is recorded.
    /// </remarks>
    public virtual Employee Employee { get; set; } = null!;

}