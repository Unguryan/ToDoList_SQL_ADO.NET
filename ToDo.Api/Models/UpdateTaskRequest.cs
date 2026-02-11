using ToDoList.Domain.Enums;

namespace ToDo.Api.Models;

public class UpdateTaskRequest
{
    public required string Title { get; set; }

    public string? Description { get; set; }

    public TaskItemStatus Status { get; set; }

    public TaskItemPriority Priority { get; set; }

    public DateTimeOffset? DueAt { get; set; }
}
