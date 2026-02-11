UPDATE task
SET title = @title, description = @description, status = @status::task_status, priority = @priority::task_priority,
    due_at = @due_at, updated_at = @updated_at, completed_at = @completed_at
WHERE id = @id;
