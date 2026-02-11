using ToDoList.Domain.Enums;

namespace ToDoList.Domain.Dto;

public class TaskFullDataDto
{
    public Guid Id { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public TaskItemStatus Status { get; set; }

    public TaskItemPriority Priority { get; set; }

    public IReadOnlyList<string> LabelNames { get; set; } = Array.Empty<string>();
}
