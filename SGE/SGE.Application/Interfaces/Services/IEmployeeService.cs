using SGE.Application.DTOs.Employees;

namespace SGE.Application.Interfaces.Services;

/// <summary>
/// Defines the service operations for managing employee data within the application.
/// </summary>
public interface IEmployeeService
{
    /// <summary>
    /// Retrieves all employees asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains an enumerable collection of employee data transfer objects (EmployeeDto).
    /// </returns>
    Task<IEnumerable<EmployeeDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an employee by their unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the employee to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the employee data transfer object (EmployeeDto) if found, otherwise null.
    /// </returns>
    Task<EmployeeDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an employee by their email address asynchronously.
    /// </summary>
    /// <param name="email">The email address of the employee to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the employee data transfer object (EmployeeDto) if found, otherwise null.
    /// </returns>
    Task<EmployeeDto?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves employees by their department identifier asynchronously.
    /// </summary>
    /// <param name="departmentId">The unique identifier of the department to filter employees by.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains an enumerable collection of employee data transfer objects (EmployeeDto).
    /// </returns>
    Task<IEnumerable<EmployeeDto>>
        GetByDepartmentAsync(int departmentId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new employee asynchronously.
    /// </summary>
    /// <param name="dto">The employee creation data transfer object containing the details of the employee to be created.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the created employee data transfer object (EmployeeDto).
    /// </returns>
    Task<EmployeeDto> CreateAsync(EmployeeCreateDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing employee asynchronously with the provided details.
    /// </summary>
    /// <param name="id">The unique identifier of the employee to be updated.</param>
    /// <param name="dto">The data transfer object containing the updated details of the employee.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result is a boolean value indicating whether the update was successful.
    /// </returns>
    Task<bool> UpdateAsync(int id, EmployeeUpdateDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an employee by their unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the employee to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a boolean indicating whether the deletion was successful.
    /// </returns>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}