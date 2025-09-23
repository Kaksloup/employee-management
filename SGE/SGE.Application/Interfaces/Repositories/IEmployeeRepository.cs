using SGE.Core.Entities;

namespace SGE.Application.Interfaces.Repositories;

/// <summary>
/// Provides an abstraction for employee-specific repository operations
/// extending the generic repository interface.
/// </summary>
public interface IEmployeeRepository : IRepository<Employee>
{
    /// <summary>
    /// Retrieves an employee entity based on the provided email address.
    /// </summary>
    /// <param name="email">The email address of the employee to be retrieved.</param>
    /// <param name="cancellationToken">A cancellation token to notify task cancellation.</param>
    /// <returns>A task representing the asynchronous operation.
    /// The task result contains the employee that matches the specified email address, or null if no match is found.</returns>
    Task<Employee?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an employee entity along with its associated department based on the provided employee ID.
    /// </summary>
    /// <param name="id">The unique identifier of the employee to be retrieved.</param>
    /// <param name="cancellationToken">A cancellation token to notify task cancellation.</param>
    /// <returns>A task representing the asynchronous operation.
    /// The task result contains the employee entity with its department data, or null if no match is found.</returns>
    Task<Employee?> GetWithDepartmentAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a paginated list of employees based on the specified page index and page size.
    /// </summary>
    /// <param name="pageIndex">The zero-based index of the page to retrieve.</param>
    /// <param name="pageSize">The number of employees to include in a single page.</param>
    /// <param name="cancellationToken">A cancellation token to notify task cancellation.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains an enumerable collection of employees for the specified page.</returns>
    Task<IEnumerable<Employee>> GetPagedAsync(int pageIndex, int pageSize,
        CancellationToken cancellationToken = default);
}