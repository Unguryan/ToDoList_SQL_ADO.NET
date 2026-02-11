using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using ToDoList.Application.Repositories;
using TodoList.ADO_NET.Repositories;
using TodoList.ADO_NET.Sql;

namespace TodoList.ADO_NET;

public static class DI
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration, string sqlQueriesBasePath)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection is missing.");

        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        var dataSource = dataSourceBuilder.Build();
        services.AddSingleton(dataSource);

        services.AddSingleton<IQueryLoader>(_ => new FileQueryLoader(sqlQueriesBasePath));

        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ILabelRepository, LabelRepository>();
        services.AddScoped<ITaskCommentRepository, TaskCommentRepository>();
        services.AddScoped<ITaskLabelRepository, TaskLabelRepository>();

        return services;
    }
}
