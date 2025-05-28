using Ardalis.Result;

namespace Application.Contracts.Services;

public interface IServiceCrud<TDto, TCreateDto, TUpdateDto>
{
    public Task<Result<List<TDto>>> FindAllAsync();
    public Task<Result<TDto>> CreateAsync(TCreateDto createDto);
    public Task<Result<TDto>> UpdateAsync(TUpdateDto updateDto);
    public Task<Result> ArchiveAsync(Guid id);
    
}