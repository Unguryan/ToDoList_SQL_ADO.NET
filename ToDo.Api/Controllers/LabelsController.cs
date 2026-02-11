using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Services;
using ToDoList.Domain.Models;
using ToDo.Api.Models;

namespace ToDo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LabelsController : ControllerBase
{
    private readonly ILabelService _labelService;

    public LabelsController(ILabelService labelService)
    {
        _labelService = labelService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Label>>> List(CancellationToken cancellationToken)
    {
        var items = await _labelService.ListAsync(cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Label>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var label = await _labelService.GetByIdAsync(id, cancellationToken);
        if (label is null)
            return NotFound();
        return Ok(label);
    }

    [HttpPost]
    public async Task<ActionResult<Label>> Create([FromBody] CreateLabelRequest request, CancellationToken cancellationToken)
    {
        var label = new Label
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Color = request.Color,
            CreatedAt = DateTimeOffset.UtcNow
        };
        await _labelService.CreateAsync(label, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = label.Id }, label);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Label>> Update(Guid id, [FromBody] UpdateLabelRequest request, CancellationToken cancellationToken)
    {
        var existing = await _labelService.GetByIdAsync(id, cancellationToken);
        if (existing is null)
            return NotFound();

        var updated = new Label
        {
            Id = id,
            Name = request.Name,
            Color = request.Color,
            CreatedAt = existing.CreatedAt
        };
        await _labelService.UpdateAsync(updated, cancellationToken);
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var existing = await _labelService.GetByIdAsync(id, cancellationToken);
        if (existing is null)
            return NotFound();
        await _labelService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
