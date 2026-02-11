DELETE FROM task_label
WHERE task_id = @task_id AND label_id = @label_id;
