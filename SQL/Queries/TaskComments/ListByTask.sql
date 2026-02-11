SELECT id, task_id, body, created_at
FROM task_comment
WHERE task_id = @task_id
ORDER BY created_at;
