# ToDo List API

Small API (pet project) – ToDo List with PostgreSQL and ADO.NET.

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) (for PostgreSQL)

## 1. Run PostgreSQL in Docker

```bash
docker run --name pg-todo-list -e POSTGRES_USER=postgres -e POSTGRES_DB=todo_list_db -e POSTGRES_PASSWORD=postgres -p 5432:5432 -d postgres:16
```

## 2. Apply migrations and seed data

From the repository root, run these in order:

**Bash / WSL / Git Bash:**

```bash
docker exec -i pg-todo-list psql -U postgres -d todo_list_db < SQL/Migrations/001_init.sql
docker exec -i pg-todo-list psql -U postgres -d todo_list_db < SQL/Migrations/002_indexes.sql
docker exec -i pg-todo-list psql -U postgres -d todo_list_db < SQL/Migrations/003_seed.sql
```

**Windows (PowerShell):**

```powershell
Get-Content .\SQL\Migrations\001_init.sql | docker exec -i pg-todo-list psql -U postgres -d todo_list_db
Get-Content .\SQL\Migrations\002_indexes.sql | docker exec -i pg-todo-list psql -U postgres -d todo_list_db
Get-Content .\SQL\Migrations\003_seed.sql | docker exec -i pg-todo-list psql -U postgres -d todo_list_db
```

## Queries (one per use case)

- **FullData** – All tasks with or without labels (one query). Returns task fields + aggregated label names per task.  
  SQL: `SQL/Queries/Tasks/FullData.sql`  
  API: `GET /api/tasks/full-data`

- **GetWithComments** – Only tasks that have at least one comment (one query). Returns task title + comment body per row.  
  SQL: `SQL/Queries/Tasks/GetWithComments.sql`  
  API: `GET /api/tasks/with-comments`

## To test (run SQL from repo root)

After running `003_seed.sql`:

**FullData (tasks + labels):**

```bash
docker exec -i pg-todo-list psql -U postgres -d todo_list_db < SQL/Queries/Tasks/FullData.sql
```

```powershell
Get-Content .\SQL\Queries\Tasks\FullData.sql | docker exec -i pg-todo-list psql -U postgres -d todo_list_db
```

**GetWithComments (tasks with comments only):**

```bash
docker exec -i pg-todo-list psql -U postgres -d todo_list_db < SQL/Queries/Tasks/GetWithComments.sql
```

```powershell
Get-Content .\SQL\Queries\Tasks\GetWithComments.sql | docker exec -i pg-todo-list psql -U postgres -d todo_list_db
```

## 3. Run the API

From the repository root:

```bash
cd ToDo.Api
dotnet run
```

**Swagger UI:** when the API is running, open `https://localhost:<port>/swagger` (or `http://localhost:<port>/swagger`) to browse and try the endpoints.

Connection string in `appsettings.Development.json` points at `localhost:5432`, database `todo_list_db`.
