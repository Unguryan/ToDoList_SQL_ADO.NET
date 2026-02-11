using System.ComponentModel;
using System.Reflection;
using ToDoList.Domain.Enums;

namespace TodoList.ADO_NET.Mapping;

internal static class EnumMapper
{
    public static string ToDbString(this TaskItemStatus value)
    {
        return GetDescription(value);
    }

    public static TaskItemStatus ToTaskStatus(string? value)
    {
        if (string.IsNullOrEmpty(value)) return TaskItemStatus.Todo;
        return ParseByDescription<TaskItemStatus>(value);
    }

    public static string ToDbString(this TaskItemPriority value)
    {
        return GetDescription(value);
    }

    public static TaskItemPriority ToTaskPriority(string? value)
    {
        if (string.IsNullOrEmpty(value)) return TaskItemPriority.Medium;
        return ParseByDescription<TaskItemPriority>(value);
    }

    private static string GetDescription(Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attr = field?.GetCustomAttribute<DescriptionAttribute>();
        return attr?.Description ?? value.ToString().ToLowerInvariant();
    }

    private static T ParseByDescription<T>(string value) where T : struct, Enum
    {
        foreach (var field in typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var attr = field.GetCustomAttribute<DescriptionAttribute>();
            if (attr?.Description == value)
                return (T)field.GetValue(null)!;
        }
        return Enum.Parse<T>(value.Replace("_", ""), ignoreCase: true); // in_progress -> InProgress
    }
}
