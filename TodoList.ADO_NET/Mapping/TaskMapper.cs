using Npgsql;
using ToDoList.Domain.Models;

namespace TodoList.ADO_NET.Mapping;

internal static class TaskMapper
{
    public static TaskItem Map(NpgsqlDataReader r)
    {
        return new TaskItem
        {
            Id = r.GetGuid(r.GetOrdinal("id")),
            Title = r.GetString(r.GetOrdinal("title")),
            Description = r.IsDBNull(r.GetOrdinal("description")) ? null : r.GetString(r.GetOrdinal("description")),
            Status = EnumMapper.ToTaskStatus(r.GetString(r.GetOrdinal("status"))),
            Priority = EnumMapper.ToTaskPriority(r.GetString(r.GetOrdinal("priority"))),
            DueAt = r.IsDBNull(r.GetOrdinal("due_at")) ? null : r.GetFieldValue<DateTimeOffset>(r.GetOrdinal("due_at")),
            CreatedAt = r.GetFieldValue<DateTimeOffset>(r.GetOrdinal("created_at")),
            UpdatedAt = r.GetFieldValue<DateTimeOffset>(r.GetOrdinal("updated_at")),
            CompletedAt = r.IsDBNull(r.GetOrdinal("completed_at")) ? null : r.GetFieldValue<DateTimeOffset>(r.GetOrdinal("completed_at"))
        };
    }
}
