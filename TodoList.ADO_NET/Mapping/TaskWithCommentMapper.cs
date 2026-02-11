using Npgsql;
using ToDoList.Domain.Dto;

namespace TodoList.ADO_NET.Mapping;

internal static class TaskWithCommentMapper
{
    public static TaskWithCommentDto Map(NpgsqlDataReader r)
    {
        return new TaskWithCommentDto
        {
            Title = r.GetString(r.GetOrdinal("title")),
            Body = r.IsDBNull(r.GetOrdinal("body")) ? null : r.GetString(r.GetOrdinal("body"))
        };
    }
}
