using ToDoList.Domain.Models;

namespace ToDoList.Application.Repositories;

public interface ITaskCommentRepository
{
    Task<IReadOnlyList<TaskItemComment>> GetByTaskIdAsync(Guid taskId, CancellationToken cancellationToken = default);
    
    Task CreateAsync(TaskItemComment comment, CancellationToken cancellationToken = default);
    
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
