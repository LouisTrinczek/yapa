using Application.Contracts.Dtos;
using Application.Contracts.Services;
using Ardalis.Result;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public abstract class BasicCrudService<TEntity, TDto, TCreateDto, TUpdateDto>(
    ILogger<BasicCrudService<TEntity, TDto, TCreateDto, TUpdateDto>> logger,
    IRepositoryCrud<TEntity> repository) : IServiceCrud<TDto, TCreateDto, TUpdateDto>
    where TEntity : BaseEntity
    where TUpdateDto : BaseUpdateDto
{
    public virtual async Task<Result<List<TDto>>> FindAllAsync()
    {
        var entities = await repository.ListAllAsync();
        return Result.Success(entities.Select(ToDto).ToList());
    }

    public virtual async Task<Result<TDto>> CreateAsync(TCreateDto createDto)
    {
        try
        {
            var entity = ToEntity(createDto);
            var createdEntity = await repository.CreateAsync(entity);
            return Result.Success(ToDto(createdEntity));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error creating entity.");
            return Result.Error(new ErrorList(["An error occurred while creating an entity."]));
        }
    }

    public virtual async Task<Result<TDto>> UpdateAsync(TUpdateDto updateDto)
    {
        var entity = await repository.FindByIdAsync(updateDto.Id);
        if (entity == null)
            return Result.Error(new ErrorList(["Entity not found."]));

        try
        {
            UpdateEntityFromDto(entity, updateDto);
            var updatedEntity = await repository.UpdateAsync(entity);
            return Result.Success(ToDto(updatedEntity));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating entity.");
            return Result.Error(new ErrorList(["An error occurred while updating an entity."]));
        }
    }

    public virtual async Task<Result> ArchiveAsync(Guid id)
    {
        try
        {
            await repository.ArchiveAsync(id);
            return Result.Success();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error archiving entity.");
            return Result.Error(new ErrorList(["An error occurred while archiving an entity."]));
        }
    }

    protected abstract TDto ToDto(TEntity entity);
    protected abstract TEntity ToEntity(TCreateDto createDto);
    protected abstract void UpdateEntityFromDto(TEntity entity, TUpdateDto updateDto);
}