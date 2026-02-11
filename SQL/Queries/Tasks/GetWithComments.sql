SELECT
  t.title,
  c.body
FROM task t
INNER JOIN task_comment c ON t.id = c.task_id
ORDER BY t.created_at DESC, c.created_at;
