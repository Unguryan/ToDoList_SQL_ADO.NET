using Npgsql;
using ToDoList.Domain.Models;

namespace TodoList.ADO_NET.Mapping;

internal static class LabelMapper
{
    public static Label Map(NpgsqlDataReader r)
    {
        return new Label
        {
            Id = r.GetGuid(r.GetOrdinal("id")),
            Name = r.GetString(r.GetOrdinal("name")),
            Color = r.IsDBNull(r.GetOrdinal("color")) ? null : r.GetString(r.GetOrdinal("color")),
            CreatedAt = r.GetFieldValue<DateTimeOffset>(r.GetOrdinal("created_at"))
        };
    }
}
