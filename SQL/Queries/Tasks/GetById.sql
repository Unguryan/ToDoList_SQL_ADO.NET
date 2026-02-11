SELECT id, title, description, status, priority, due_at, created_at, updated_at, completed_at
FROM task
WHERE id = @id;
