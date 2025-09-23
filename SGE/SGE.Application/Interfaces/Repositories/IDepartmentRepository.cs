using SGE.Core.Entities;

namespace SGE.Application.Interfaces.Repositories;

/// <summary>
/// Represents a repository interface for managing <see cref="Department"/> entities.
/// Provides additional methods specific to operations on departments.
/// Inherits from <see cref="IRepository{T}"/> where T is <see cref="Department"/>.
/// </summary>
public interface IDepartmentRepository : IRepository<Department>
{
    /// <summary>
    /// Asynchronously retrieves a <see cref="Department"/> entity by its name.
    /// </summary>
    /// <param name="name">The name of the department to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the <see cref="Department"/> entity if found; otherwise, null.</returns>
    Task<Department?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves a <see cref="Department"/> entity by its unique code.
    /// </summary>
    /// <param name="code">The unique code of the department to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the <see cref="Department"/> entity if found; otherwise, null.</returns>
    Task<Department?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
}