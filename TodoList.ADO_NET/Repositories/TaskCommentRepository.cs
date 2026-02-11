using Npgsql;
using ToDoList.Application.Repositories;
using ToDoList.Domain.Models;
using TodoList.ADO_NET.Mapping;
using TodoList.ADO_NET.Sql;

namespace TodoList.ADO_NET.Repositories;

public sealed class TaskCommentRepository : ITaskCommentRepository
{
    private readonly NpgsqlDataSource _dataSource;
    private readonly IQueryLoader _queries;

    public TaskCommentRepository(NpgsqlDataSource dataSource, IQueryLoader queries)
    {
        _dataSource = dataSource;
        _queries = queries;
    }

    public async Task<IReadOnlyList<TaskItemComment>> GetByTaskIdAsync(Guid taskId, CancellationToken cancellationToken = default)
    {
        var sql = _queries.Load("TaskComments/ListByTask.sql");
        await using var conn = await _dataSource.OpenConnectionAsync(cancellationToken);
        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("task_id", taskId);
        await using var r = await cmd.ExecuteReaderAsync(cancellationToken);
        var list = new List<TaskItemComment>();
        while (await r.ReadAsync(cancellationToken))
            list.Add(TaskCommentMapper.Map(r));
        return list;
    }

    public async Task CreateAsync(TaskItemComment comment, CancellationToken cancellationToken = default)
    {
        var sql = _queries.Load("TaskComments/Insert.sql");
        await using var conn = await _dataSource.OpenConnectionAsync(cancellationToken);
        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("id", comment.Id);
        cmd.Parameters.AddWithValue("task_id", comment.TaskId);
        cmd.Parameters.AddWithValue("body", comment.Body);
        cmd.Parameters.AddWithValue("created_at", comment.CreatedAt);
        await cmd.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sql = _queries.Load("TaskComments/Delete.sql");
        await using var conn = await _dataSource.OpenConnectionAsync(cancellationToken);
        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("id", id);
        await cmd.ExecuteNonQueryAsync(cancellationToken);
    }
}
