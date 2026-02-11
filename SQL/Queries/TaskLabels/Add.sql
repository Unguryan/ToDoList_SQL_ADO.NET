INSERT INTO task_label (task_id, label_id)
VALUES (@task_id, @label_id)
ON CONFLICT (task_id, label_id) DO NOTHING;
