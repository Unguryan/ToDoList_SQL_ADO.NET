namespace ToDo.Api.Models;

public class UpdateLabelRequest
{
    public required string Name { get; set; }

    public string? Color { get; set; }
}
