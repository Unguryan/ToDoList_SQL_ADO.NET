using ToDoList.Domain.Models;

namespace ToDoList.Application.Services;

public interface ILabelService
{
    Task<Label?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Label>> ListAsync(CancellationToken cancellationToken = default);

    Task CreateAsync(Label label, CancellationToken cancellationToken = default);

    Task UpdateAsync(Label label, CancellationToken cancellationToken = default);
    
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
