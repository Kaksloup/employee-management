using SGE.Core.Entities;

namespace SGE.Application.Interfaces.Repositories;

/// <summary>
/// Represents a repository interface for handling data access operations specific to LeaveRequest entities.
/// Provides methods for retrieving LeaveRequest data with additional customization for employee-specific requests.
/// </summary>
public interface ILeaveRequestRepository: IRepository<LeaveRequest>
{
    /// <summary>
    /// Asynchronously retrieves a collection of leave requests associated with a specific employee.
    /// </summary>
    /// <param name="employeeId">The unique identifier of the employee whose leave requests are to be fetched.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to observe the cancellation request.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains an enumerable collection of leave requests associated with the specified employee.</returns>
    Task<IEnumerable<LeaveRequest>> GetByEmployeeAsync(int employeeId, CancellationToken cancellationToken = default);
}