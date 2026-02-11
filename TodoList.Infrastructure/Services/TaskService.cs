using ToDoList.Application.Repositories;
using ToDoList.Application.Services;

namespace TodoList.Infrastructure.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    //Business logic can be added here
    public Task<ToDoList.Domain.Models.TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => _taskRepository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<ToDoList.Domain.Models.TaskItem>> ListAsync(CancellationToken cancellationToken = default)
        => _taskRepository.ListAsync(cancellationToken);

    public Task<IReadOnlyList<ToDoList.Domain.Dto.TaskFullDataDto>> GetFullDataAsync(CancellationToken cancellationToken = default)
        => _taskRepository.GetFullDataAsync(cancellationToken);

    public Task<IReadOnlyList<ToDoList.Domain.Dto.TaskWithCommentDto>> GetWithCommentsAsync(CancellationToken cancellationToken = default)
        => _taskRepository.GetWithCommentsAsync(cancellationToken);

    public Task CreateAsync(ToDoList.Domain.Models.TaskItem task, CancellationToken cancellationToken = default)
        => _taskRepository.CreateAsync(task, cancellationToken);

    public Task UpdateAsync(ToDoList.Domain.Models.TaskItem task, CancellationToken cancellationToken = default)
        => _taskRepository.UpdateAsync(task, cancellationToken);

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => _taskRepository.DeleteAsync(id, cancellationToken);
}
