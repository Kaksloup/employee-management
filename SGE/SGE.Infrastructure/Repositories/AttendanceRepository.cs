using Microsoft.EntityFrameworkCore;
using SGE.Application.Interfaces.Repositories;
using SGE.Core.Entities;
using SGE.Infrastructure.Data;

namespace SGE.Infrastructure.Repositories;

public class AttendanceRepository : Repository<Attendance>, IAttendanceRepository
{
    /// <summary>
    /// Defines a repository that provides data access capabilities for Attendance entities.
    /// Extends the generic Repository base class and implements the IAttendanceRepository interface.
    /// </summary>
    public AttendanceRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Retrieves attendance records associated with a specific employee from the data source.
    /// </summary>
    /// <param name="employeeId">The unique identifier of the employee whose attendance records are to be fetched.</param>
    /// <param name="cancellationToken">An optional token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, with a collection of Attendance entities for the specified employee.</returns>
    public async Task<IEnumerable<Attendance>> GetByEmployeeAsync(int employeeId,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(a => a.EmployeeId == employeeId)
            .Include(a => a.Employee)
            .ToListAsync(cancellationToken);
    }
}