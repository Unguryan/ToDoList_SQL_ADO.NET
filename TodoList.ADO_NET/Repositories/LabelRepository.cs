using Npgsql;
using ToDoList.Application.Repositories;
using ToDoList.Domain.Models;
using TodoList.ADO_NET.Mapping;
using TodoList.ADO_NET.Sql;

namespace TodoList.ADO_NET.Repositories;

public sealed class LabelRepository : ILabelRepository
{
    private readonly NpgsqlDataSource _dataSource;
    private readonly IQueryLoader _queries;

    public LabelRepository(NpgsqlDataSource dataSource, IQueryLoader queries)
    {
        _dataSource = dataSource;
        _queries = queries;
    }

    public async Task<Label?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sql = _queries.Load("Labels/GetById.sql");
        await using var conn = await _dataSource.OpenConnectionAsync(cancellationToken);
        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("id", id);
        await using var r = await cmd.ExecuteReaderAsync(cancellationToken);
        return await r.ReadAsync(cancellationToken) ? LabelMapper.Map(r) : null;
    }

    public async Task<IReadOnlyList<Label>> ListAsync(CancellationToken cancellationToken = default)
    {
        var sql = _queries.Load("Labels/List.sql");
        await using var conn = await _dataSource.OpenConnectionAsync(cancellationToken);
        await using var cmd = new NpgsqlCommand(sql, conn);
        await using var r = await cmd.ExecuteReaderAsync(cancellationToken);
        var list = new List<Label>();
        while (await r.ReadAsync(cancellationToken))
            list.Add(LabelMapper.Map(r));
        return list;
    }

    public async Task CreateAsync(Label label, CancellationToken cancellationToken = default)
    {
        var sql = _queries.Load("Labels/Insert.sql");
        await using var conn = await _dataSource.OpenConnectionAsync(cancellationToken);
        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("id", label.Id);
        cmd.Parameters.AddWithValue("name", label.Name);
        cmd.Parameters.AddWithValue("color", (object?)label.Color ?? DBNull.Value);
        cmd.Parameters.AddWithValue("created_at", label.CreatedAt);
        await cmd.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task UpdateAsync(Label label, CancellationToken cancellationToken = default)
    {
        var sql = _queries.Load("Labels/Update.sql");
        await using var conn = await _dataSource.OpenConnectionAsync(cancellationToken);
        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("id", label.Id);
        cmd.Parameters.AddWithValue("name", label.Name);
        cmd.Parameters.AddWithValue("color", (object?)label.Color ?? DBNull.Value);
        await cmd.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sql = _queries.Load("Labels/Delete.sql");
        await using var conn = await _dataSource.OpenConnectionAsync(cancellationToken);
        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("id", id);
        await cmd.ExecuteNonQueryAsync(cancellationToken);
    }
}
