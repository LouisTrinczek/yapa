using Domain.Entities;

namespace Domain.Repositories;

public interface IRepositoryCrud<TEntity> where TEntity : BaseEntity
{
    public Task<List<TEntity>> ListAllAsync();
    public Task<TEntity?> FindByIdAsync(Guid id);
    public Task<TEntity> CreateAsync(TEntity entity);
    public Task<TEntity> UpdateAsync(TEntity entity);
    public Task ArchiveAsync(Guid id);
}