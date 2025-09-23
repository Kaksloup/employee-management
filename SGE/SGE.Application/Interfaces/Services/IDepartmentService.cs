using SGE.Application.DTOs;

namespace SGE.Application.Interfaces.Services;

/// <summary>
/// Defines the service contract for managing departments.
/// </summary>
public interface IDepartmentService
{
    /// <summary>
    /// Asynchronously retrieves all departments.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains an enumerable collection of <see cref="DepartmentDto"/>.</returns>
    Task<IEnumerable<DepartmentDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves a department by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the department to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the <see cref="DepartmentDto"/> for the specified identifier, or null if not found.</returns>
    Task<DepartmentDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously creates a new department.
    /// </summary>
    /// <param name="dto">The data transfer object containing the details of the department to create.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the newly created <see cref="DepartmentDto"/>.</returns>
    Task<DepartmentDto> CreateAsync(DepartmentCreateDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously updates an existing department with the specified identifier and update details.
    /// </summary>
    /// <param name="id">The unique identifier of the department to update.</param>
    /// <param name="dto">The data transfer object containing the updated details of the department.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a boolean value indicating whether the update was successful.</returns>
    Task<bool> UpdateAsync(int id, DepartmentUpdateDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes a department by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the department to delete.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}