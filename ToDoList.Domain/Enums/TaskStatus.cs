using System.ComponentModel;

namespace ToDoList.Domain.Enums;

public enum TaskStatus
{
    [Description("todo")]
    Todo = 0,

    [Description("in_progress")]
    InProgress = 1,

    [Description("done")]
    Done = 2,

    [Description("canceled")]
    Canceled = 3
}
