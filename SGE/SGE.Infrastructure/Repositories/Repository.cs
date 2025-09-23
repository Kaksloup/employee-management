using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SGE.Application.Interfaces.Repositories;
using SGE.Infrastructure.Data;

namespace SGE.Infrastructure.Repositories;

/// <summary>
/// Represents the base implementation of a repository for performing CRUD operations
/// on entities of type <typeparamref name="T"/>. This repository interacts with the
/// Entity Framework Core DbContext and provides a common interface for handling data access.
/// </summary>
/// <typeparam name="T">The type of the entity managed by the repository.</typeparam>
public class Repository<T> : IRepository<T> where T : class
{
    /// <summary>
    /// The DbContext instance used to interact with the underlying database.
    /// This context provides access to the Entity Framework Core's DbSet properties
    /// and is used to perform database operations such as querying, inserting, updating, and deletion.
    /// </summary>
    protected readonly ApplicationDbContext _context;

    /// <summary>
    /// Represents the DbSet instance for the specified entity type <typeparamref name="T"/>.
    /// Used to perform CRUD operations and query the database for entities of type <typeparamref name="T"/>.
    /// Acts as a gateway to the underlying database table corresponding to the entity type.
    /// </summary>
    protected readonly DbSet<T> _dbSet;

    /// <summary>
    /// A base repository class for managing data access using Entity Framework Core.
    /// Provides a set of common methods for CRUD operations on entities.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the entity that is handled by the repository.
    /// Must be a reference type.
    /// </typeparam>
    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    /// <inheritdoc/>   
    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync([id], cancellationToken);
    }

    /// <inheritdoc/>   
    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
    }

    /// <inheritdoc/>   
    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(predicate).AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
    }

    /// <inheritdoc/>   
    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>   
    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    /// <inheritdoc/>   
    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync([id], cancellationToken) != null;
    }

    /// <inheritdoc/>   
    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbSet.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}