namespace ToDoList.Application.Repositories;

public interface ITaskLabelRepository
{
    Task<IReadOnlyList<Guid>> GetLabelIdsByTaskIdAsync(Guid taskId, CancellationToken cancellationToken = default);

    Task AddAsync(Guid taskId, Guid labelId, CancellationToken cancellationToken = default);

    Task RemoveAsync(Guid taskId, Guid labelId, CancellationToken cancellationToken = default);
}
