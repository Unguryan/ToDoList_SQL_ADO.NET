INSERT INTO task (id, title, description, status, priority, due_at, created_at, updated_at, completed_at)
VALUES (@id, @title, @description, @status::task_status, @priority::task_priority, @due_at, @created_at, @updated_at, @completed_at);
