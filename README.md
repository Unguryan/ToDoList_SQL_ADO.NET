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

## To test

After running `003_seed.sql`, from the **repository root** you can run the FullData query:

**Bash:**

```bash
docker exec -i pg-todo-list psql -U postgres -d todo_list_db < SQL/Queries/Tasks/FullData.sql
```

**PowerShell:**

```powershell
Get-Content .\SQL\Queries\Tasks\FullData.sql | docker exec -i pg-todo-list psql -U postgres -d todo_list_db
```

## 3. Run the API

From the repository root:

```bash
cd ToDo.Api
dotnet run
```

Connection string in `appsettings.Development.json` points at `localhost:5432`, database `todo_list_db`.
