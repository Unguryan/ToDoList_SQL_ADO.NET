using ToDoList.Domain.Models;

namespace ToDoList.Application.Services;

public interface ITaskCommentService
{
    Task<IReadOnlyList<TaskItemComment>> GetByTaskIdAsync(Guid taskId, CancellationToken cancellationToken = default);

    Task CreateAsync(TaskItemComment comment, CancellationToken cancellationToken = default);
    
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
