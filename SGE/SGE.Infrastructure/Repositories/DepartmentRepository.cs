using Microsoft.EntityFrameworkCore;
using SGE.Application.Interfaces.Repositories;
using SGE.Core.Entities;
using SGE.Infrastructure.Data;

namespace SGE.Infrastructure.Repositories;

/// <summary>
/// Provides database operations for the Department entity. Implements methods
/// for retrieving departments by their name or code, including all inherited
/// methods from the generic repository.
/// </summary>
public class DepartmentRepository : Repository<Department>, IDepartmentRepository
{
    /// <summary>
    /// Repository implementation for handling Department-related database operations.
    /// Extends the generic repository for common database methods and provides
    /// additional methods specific to the Department entity, such as retrieving
    /// departments by name or code.
    /// </summary>
    public DepartmentRepository(ApplicationDbContext context) : base(context) { }

    /// <summary>
    /// Retrieves a department entity based on its name.
    /// Searches the database for a department with a matching name and returns the entity if found.
    /// </summary>
    /// <param name="name">The name of the department to retrieve.</param>
    /// <param name="cancellationToken">Optional cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains the department entity if found; otherwise, null.</returns>
    public async Task<Department?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(d => d.Name == name, cancellationToken);
    }

    /// <summary>
    /// Retrieves a department entity that matches the specified code.
    /// </summary>
    /// <param name="code">The code of the department to retrieve.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task representing the asynchronous operation. The task result contains
    /// the <see cref="Department"/> entity that matches the specified code, or null if no match is found.
    /// </returns>
    public async Task<Department?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(d => d.Code == code, cancellationToken);
    }
}