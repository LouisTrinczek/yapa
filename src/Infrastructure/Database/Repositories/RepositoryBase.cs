using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public abstract class RepositoryBase<TEntity> : IRepositoryCrud<TEntity> where TEntity : BaseEntity
{
    protected readonly ProjectDatabaseContext DbContext;
    protected DbSet<TEntity> Set => DbContext.Set<TEntity>();

    protected RepositoryBase(ProjectDatabaseContext context) => DbContext = context;

    public virtual async Task<List<TEntity>> ListAllAsync() =>
        await Set.Where(x => x.ArchivedAt == null).ToListAsync();

    public virtual async Task<TEntity?> FindByIdAsync(Guid id) =>
        await Set.FindAsync(id);

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        var entityEntry = await Set.AddAsync(entity);
        await DbContext.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        DbContext.Entry(entity).State = EntityState.Modified;
        await DbContext.SaveChangesAsync();
        return entity;
    }

    public virtual async Task ArchiveAsync(Guid id)
    {
        var entity = await Archive(id);
        if (entity != null)
        {
            await DbContext.SaveChangesAsync();
        }
    }

    protected virtual async Task<TEntity?> Archive(Guid id)
    {
        var entity = await FindByIdAsync(id);
        if (entity != null)
        {
            entity.Archive(); // Assuming Archive() is a method in BaseEntity
            await DbContext.SaveChangesAsync();
        }
        return entity;
    }
}