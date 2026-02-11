using ToDoList.Domain.Enums;

namespace ToDoList.Domain.Models;

public class TaskItem
{
    public Guid Id { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public TaskItemStatus Status { get; set; } = TaskItemStatus.Todo;

    public TaskItemPriority Priority { get; set; } = TaskItemPriority.Medium;

    public DateTimeOffset? DueAt { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public DateTimeOffset? CompletedAt { get; set; }
}
