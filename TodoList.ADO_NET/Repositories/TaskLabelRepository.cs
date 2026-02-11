using Npgsql;
using ToDoList.Application.Repositories;
using TodoList.ADO_NET.Sql;

namespace TodoList.ADO_NET.Repositories;

public sealed class TaskLabelRepository : ITaskLabelRepository
{
    private readonly NpgsqlDataSource _dataSource;
    private readonly IQueryLoader _queries;

    public TaskLabelRepository(NpgsqlDataSource dataSource, IQueryLoader queries)
    {
        _dataSource = dataSource;
        _queries = queries;
    }

    public async Task<IReadOnlyList<Guid>> GetLabelIdsByTaskIdAsync(Guid taskId, CancellationToken cancellationToken = default)
    {
        var sql = _queries.Load("TaskLabels/GetByTask.sql");
        await using var conn = await _dataSource.OpenConnectionAsync(cancellationToken);
        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("task_id", taskId);
        await using var r = await cmd.ExecuteReaderAsync(cancellationToken);
        var list = new List<Guid>();
        while (await r.ReadAsync(cancellationToken))
            list.Add(r.GetGuid(r.GetOrdinal("label_id")));
        return list;
    }

    public async Task AddAsync(Guid taskId, Guid labelId, CancellationToken cancellationToken = default)
    {
        var sql = _queries.Load("TaskLabels/Add.sql");
        await using var conn = await _dataSource.OpenConnectionAsync(cancellationToken);
        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("task_id", taskId);
        cmd.Parameters.AddWithValue("label_id", labelId);
        await cmd.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task RemoveAsync(Guid taskId, Guid labelId, CancellationToken cancellationToken = default)
    {
        var sql = _queries.Load("TaskLabels/Remove.sql");
        await using var conn = await _dataSource.OpenConnectionAsync(cancellationToken);
        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("task_id", taskId);
        cmd.Parameters.AddWithValue("label_id", labelId);
        await cmd.ExecuteNonQueryAsync(cancellationToken);
    }
}
