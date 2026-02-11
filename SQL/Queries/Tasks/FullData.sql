
SELECT 
  t.id,
  t.title,
  t.description,
  t.status,
  t.priority,
  COALESCE(array_agg(l.name) FILTER (WHERE l.name IS NOT NULL), ARRAY[]::text[]) as label_names
FROM task t
LEFT JOIN task_label tl ON t.id = tl.task_id
LEFT JOIN label l ON tl.label_id = l.id
GROUP BY t.id, t.title, t.description, t.status, t.priority, t.created_at
ORDER BY t.created_at DESC;
