using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.Services;
using TodoList.Infrastructure.Services;

namespace TodoList.Infrastructure;

public static class DI
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<ITaskCommentService, TaskCommentService>();
        services.AddScoped<ILabelService, LabelService>();
        return services;
    }
}
