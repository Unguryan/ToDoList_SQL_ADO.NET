using TodoList.ADO_NET;
using TodoList.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var sqlQueriesPath = Path.Combine(AppContext.BaseDirectory, "Sql", "Queries");
builder.Services.AddDatabase(builder.Configuration, sqlQueriesPath);
builder.Services.AddInfrastructure();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
