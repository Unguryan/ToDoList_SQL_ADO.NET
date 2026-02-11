using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Services;
using ToDoList.Domain.Enums;
using ToDoList.Domain.Models;
using ToDo.Api.Models;

namespace ToDo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TaskItem>>> List(CancellationToken cancellationToken)
    {
        var items = await _taskService.ListAsync(cancellationToken);
        return Ok(items);
    }

    [HttpGet("full-data")]
    public async Task<ActionResult<IReadOnlyList<ToDoList.Domain.Dto.TaskFullDataDto>>> GetFullData(CancellationToken cancellationToken)
    {
        var items = await _taskService.GetFullDataAsync(cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskItem>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var task = await _taskService.GetByIdAsync(id, cancellationToken);
        if (task is null)
            return NotFound();
        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<TaskItem>> Create([FromBody] CreateTaskRequest request, CancellationToken cancellationToken)
    {
        var now = DateTimeOffset.UtcNow;
        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            Status = request.Status,
            Priority = request.Priority,
            DueAt = request.DueAt,
            CreatedAt = now,
            UpdatedAt = now,
            CompletedAt = request.Status == TaskItemStatus.Done ? now : null
        };
        await _taskService.CreateAsync(task, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TaskItem>> Update(Guid id, [FromBody] UpdateTaskRequest request, CancellationToken cancellationToken)
    {
        var existing = await _taskService.GetByIdAsync(id, cancellationToken);
        if (existing is null)
            return NotFound();

        var updated = new TaskItem
        {
            Id = id,
            Title = request.Title,
            Description = request.Description,
            Status = request.Status,
            Priority = request.Priority,
            DueAt = request.DueAt,
            CreatedAt = existing.CreatedAt,
            UpdatedAt = DateTimeOffset.UtcNow,
            CompletedAt = request.Status == TaskItemStatus.Done ? DateTimeOffset.UtcNow : null
        };
        await _taskService.UpdateAsync(updated, cancellationToken);
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var existing = await _taskService.GetByIdAsync(id, cancellationToken);
        if (existing is null)
            return NotFound();
        await _taskService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
