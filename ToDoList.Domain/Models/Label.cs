namespace ToDoList.Domain.Models;

public class Label
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public string? Color { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}
