using ToDoList.Application.Repositories;
using ToDoList.Application.Services;
using ToDoList.Domain.Models;

namespace TodoList.Infrastructure.Services;

public sealed class TaskCommentService : ITaskCommentService
{
    private readonly ITaskCommentRepository _taskCommentRepository;

    public TaskCommentService(ITaskCommentRepository taskCommentRepository)
    {
        _taskCommentRepository = taskCommentRepository;
    }

    //Business logic can be added here
    public Task<IReadOnlyList<TaskItemComment>> GetByTaskIdAsync(Guid taskId, CancellationToken cancellationToken = default)
        => _taskCommentRepository.GetByTaskIdAsync(taskId, cancellationToken);

    public Task CreateAsync(TaskItemComment comment, CancellationToken cancellationToken = default)
        => _taskCommentRepository.CreateAsync(comment, cancellationToken);

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => _taskCommentRepository.DeleteAsync(id, cancellationToken);
}
