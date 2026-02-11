namespace ToDoList.Domain.Dto;

public class TaskWithCommentDto
{
    public required string Title { get; set; }
    
    public string? Body { get; set; }
}
