using ToDoList.Domain.Dto;
using ToDoList.Domain.Models;

namespace ToDoList.Application.Repositories;

public interface ITaskRepository
{
    Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TaskItem>> ListAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TaskFullDataDto>> GetFullDataAsync(CancellationToken cancellationToken = default);

    Task CreateAsync(TaskItem task, CancellationToken cancellationToken = default);

    Task UpdateAsync(TaskItem task, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
