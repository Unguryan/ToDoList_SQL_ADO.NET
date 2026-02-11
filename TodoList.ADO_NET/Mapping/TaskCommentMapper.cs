using Npgsql;
using ToDoList.Domain.Models;

namespace TodoList.ADO_NET.Mapping;

internal static class TaskCommentMapper
{
    public static TaskItemComment Map(NpgsqlDataReader r)
    {
        return new TaskItemComment
        {
            Id = r.GetGuid(r.GetOrdinal("id")),
            TaskId = r.GetGuid(r.GetOrdinal("task_id")),
            Body = r.GetString(r.GetOrdinal("body")),
            CreatedAt = r.GetFieldValue<DateTimeOffset>(r.GetOrdinal("created_at"))
        };
    }
}
