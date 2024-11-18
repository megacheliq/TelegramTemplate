using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Abstract;

/// <summary>
/// Base of all repositories
/// </summary>
public abstract class RepositoryBase<TEntity> where TEntity : class
{
    protected readonly AppDbContext Context;
    private readonly DbSet<TEntity> _dbSet;

    protected RepositoryBase(AppDbContext context)
    {
        Context = context;
        _dbSet = Context.Set<TEntity>();
    }

    /// <summary>
    /// Get by identifier
    /// </summary>
    /// <param name="id">Identifier</param>
    public virtual async Task<TEntity?> GetByIdAsync(long id)
    {
        return await _dbSet.FindAsync(id);
    }

    /// <summary>
    /// Get all
    /// </summary>
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    /// <summary>
    /// Create in database
    /// </summary>
    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        var created = await _dbSet.AddAsync(entity);
        await Context.SaveChangesAsync();
        return created.Entity;
    }

    /// <summary>
    /// Update in database
    /// </summary>
    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var updated = _dbSet.Update(entity);
        await Context.SaveChangesAsync();
        return updated.Entity;
    }

    /// <summary>
    /// Delete from database
    /// </summary>
    public virtual async Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        await Context.SaveChangesAsync();
    }
}