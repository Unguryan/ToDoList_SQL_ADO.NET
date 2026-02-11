using System.ComponentModel;

namespace ToDoList.Domain.Enums;

public enum TaskPriority
{
    [Description("low")]
    Low = 0,

    [Description("medium")]
    Medium = 1,

    [Description("high")]
    High = 2,

    [Description("critical")]
    Critical = 3
}
