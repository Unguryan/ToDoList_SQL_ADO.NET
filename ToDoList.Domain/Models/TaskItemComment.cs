namespace ToDoList.Domain.Models;

public class TaskItemComment
{
    public Guid Id { get; set; }

    public Guid TaskId { get; set; }

    public required string Body { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}
