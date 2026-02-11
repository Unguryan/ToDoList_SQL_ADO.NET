using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Services;
using ToDoList.Domain.Models;
using ToDo.Api.Models;

namespace ToDo.Api.Controllers;

[ApiController]
[Route("api/tasks/{taskId:guid}/comments")]
public class TaskCommentsController : ControllerBase
{
    private readonly ITaskCommentService _taskCommentService;
    private readonly ITaskService _taskService;

    public TaskCommentsController(ITaskCommentService taskCommentService, ITaskService taskService)
    {
        _taskCommentService = taskCommentService;
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TaskItemComment>>> List(Guid taskId, CancellationToken cancellationToken)
    {
        var task = await _taskService.GetByIdAsync(taskId, cancellationToken);
        if (task is null)
            return NotFound();

        var comments = await _taskCommentService.GetByTaskIdAsync(taskId, cancellationToken);
        return Ok(comments);
    }

    [HttpPost]
    public async Task<ActionResult<TaskItemComment>> Create(Guid taskId, [FromBody] CreateTaskCommentRequest request, CancellationToken cancellationToken)
    {
        var task = await _taskService.GetByIdAsync(taskId, cancellationToken);
        if (task is null)
            return NotFound();

        var comment = new TaskItemComment
        {
            Id = Guid.NewGuid(),
            TaskId = taskId,
            Body = request.Body,
            CreatedAt = DateTimeOffset.UtcNow
        };
        await _taskCommentService.CreateAsync(comment, cancellationToken);
        return Created($"/api/tasks/{taskId}/comments/{comment.Id}", comment);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid taskId, Guid id, CancellationToken cancellationToken)
    {
        var comments = await _taskCommentService.GetByTaskIdAsync(taskId, cancellationToken);
        var comment = comments.FirstOrDefault(c => c.Id == id);
        if (comment is null)
            return NotFound();

        await _taskCommentService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
