using ToDoList.Domain.Enums;

namespace ToDo.Api.Models;

public class CreateTaskRequest
{
    public required string Title { get; set; }

    public string? Description { get; set; }

    public TaskItemStatus Status { get; set; } = TaskItemStatus.Todo;

    public TaskItemPriority Priority { get; set; } = TaskItemPriority.Medium;

    public DateTimeOffset? DueAt { get; set; }
}
