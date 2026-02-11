using ToDoList.Application.Repositories;
using ToDoList.Application.Services;
using ToDoList.Domain.Models;

namespace TodoList.Infrastructure.Services;

public class LabelService : ILabelService
{
    private readonly ILabelRepository _labelRepository;

    public LabelService(ILabelRepository labelRepository)
    {
        _labelRepository = labelRepository;
    }

    //Business logic can be added here
    public Task<Label?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => _labelRepository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<Label>> ListAsync(CancellationToken cancellationToken = default)
        => _labelRepository.ListAsync(cancellationToken);

    public Task CreateAsync(Label label, CancellationToken cancellationToken = default)
        => _labelRepository.CreateAsync(label, cancellationToken);

    public Task UpdateAsync(Label label, CancellationToken cancellationToken = default)
        => _labelRepository.UpdateAsync(label, cancellationToken);

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => _labelRepository.DeleteAsync(id, cancellationToken);
}
