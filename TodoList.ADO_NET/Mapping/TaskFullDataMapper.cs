using Npgsql;
using ToDoList.Domain.Dto;

namespace TodoList.ADO_NET.Mapping;

internal static class TaskFullDataMapper
{
    public static TaskFullDataDto Map(NpgsqlDataReader r)
    {
        var labelNamesOrdinal = r.GetOrdinal("label_names");
        string[] labelNames;
        if (r.IsDBNull(labelNamesOrdinal))
        {
            labelNames = Array.Empty<string>();
        }
        else
        {
            labelNames = r.GetFieldValue<string[]>(labelNamesOrdinal);
        }

        return new TaskFullDataDto
        {
            Id = r.GetGuid(r.GetOrdinal("id")),
            Title = r.GetString(r.GetOrdinal("title")),
            Description = r.IsDBNull(r.GetOrdinal("description")) ? null : r.GetString(r.GetOrdinal("description")),
            Status = EnumMapper.ToTaskStatus(r.GetString(r.GetOrdinal("status"))),
            Priority = EnumMapper.ToTaskPriority(r.GetString(r.GetOrdinal("priority"))),
            LabelNames = labelNames
        };
    }
}
