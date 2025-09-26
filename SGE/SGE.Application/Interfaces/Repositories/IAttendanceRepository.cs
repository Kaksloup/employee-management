using SGE.Core.Entities;

namespace SGE.Application.Interfaces.Repositories;

/// <summary>
/// Provides an interface for managing attendance records in the system,
/// offering functionality to interact with attendance data specific to employees.
/// </summary>
public interface IAttendanceRepository: IRepository<Attendance>
{
    /// <summary>
    /// Retrieves attendance records for a specific employee asynchronously.
    /// </summary>
    /// <param name="employeeId">The unique identifier of the employee whose attendance records are to be retrieved.</param>
    /// <param name="cancellationToken">A token for propagating notification that the operation should be canceled.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="Attendance"/> records for the specified employee.</returns>
    Task<IEnumerable<Attendance>> GetByEmployeeAsync(int employeeId, CancellationToken cancellationToken = default);
}
