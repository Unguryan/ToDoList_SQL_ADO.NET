using Npgsql;
using ToDoList.Application.Repositories;
using ToDoList.Domain.Models;
using TodoList.ADO_NET.Mapping;
using TodoList.ADO_NET.Sql;
using ToDoList.Domain.Dto;

namespace TodoList.ADO_NET.Repositories;

public sealed class TaskRepository : ITaskRepository
{
    private readonly NpgsqlDataSource _dataSource;
    private readonly IQueryLoader _queries;

    public TaskRepository(NpgsqlDataSource dataSource, IQueryLoader queries)
    {
        _dataSource = dataSource;
        _queries = queries;
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sql = _queries.Load("Tasks/GetById.sql");
        await using var conn = await _dataSource.OpenConnectionAsync(cancellationToken);
        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("id", id);
        await using var r = await cmd.ExecuteReaderAsync(cancellationToken);
        return await r.ReadAsync(cancellationToken) ? TaskMapper.Map(r) : null;
    }

    public async Task<IReadOnlyList<TaskItem>> ListAsync(CancellationToken cancellationToken = default)
    {
        var sql = _queries.Load("Tasks/List.sql");
        await using var conn = await _dataSource.OpenConnectionAsync(cancellationToken);
        await using var cmd = new NpgsqlCommand(sql, conn);
        await using var r = await cmd.ExecuteReaderAsync(cancellationToken);
        var list = new List<TaskItem>();
        while (await r.ReadAsync(cancellationToken))
            list.Add(TaskMapper.Map(r));
        return list;
    }

    public async Task<IReadOnlyList<TaskFullDataDto>> GetFullDataAsync(CancellationToken cancellationToken = default)
    {
        var sql = _queries.Load("Tasks/FullData.sql");
        await using var conn = await _dataSource.OpenConnectionAsync(cancellationToken);
        await using var cmd = new NpgsqlCommand(sql, conn);
        await using var r = await cmd.ExecuteReaderAsync(cancellationToken);
        var list = new List<TaskFullDataDto>();
        while (await r.ReadAsync(cancellationToken))
            list.Add(TaskFullDataMapper.Map(r));
        return list;
    }

    public async Task CreateAsync(TaskItem task, CancellationToken cancellationToken = default)
    {
        var sql = _queries.Load("Tasks/Insert.sql");
        await using var conn = await _dataSource.OpenConnectionAsync(cancellationToken);
        await using var cmd = new NpgsqlCommand(sql, conn);
        AddTaskParams(cmd, task);
        await cmd.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task UpdateAsync(TaskItem task, CancellationToken cancellationToken = default)
    {
        var sql = _queries.Load("Tasks/Update.sql");
        await using var conn = await _dataSource.OpenConnectionAsync(cancellationToken);
        await using var cmd = new NpgsqlCommand(sql, conn);
        AddTaskParams(cmd, task);
        await cmd.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sql = _queries.Load("Tasks/Delete.sql");
        await using var conn = await _dataSource.OpenConnectionAsync(cancellationToken);
        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("id", id);
        await cmd.ExecuteNonQueryAsync(cancellationToken);
    }

    private static void AddTaskParams(NpgsqlCommand cmd, TaskItem task)
    {
        cmd.Parameters.AddWithValue("id", task.Id);
        cmd.Parameters.AddWithValue("title", task.Title);
        cmd.Parameters.AddWithValue("description", (object?)task.Description ?? DBNull.Value);
        cmd.Parameters.AddWithValue("status", task.Status.ToDbString());
        cmd.Parameters.AddWithValue("priority", task.Priority.ToDbString());
        cmd.Parameters.AddWithValue("due_at", (object?)task.DueAt ?? DBNull.Value);
        cmd.Parameters.AddWithValue("created_at", task.CreatedAt);
        cmd.Parameters.AddWithValue("updated_at", task.UpdatedAt);
        cmd.Parameters.AddWithValue("completed_at", (object?)task.CompletedAt ?? DBNull.Value);
    }
}
