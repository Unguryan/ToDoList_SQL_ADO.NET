-- =============================================
-- ToDo List API – Indexes (run after 001_init.sql)
-- =============================================

CREATE INDEX ix_task_status ON task(status);

CREATE INDEX ix_task_due_at ON task(due_at);

CREATE INDEX ix_comment_task_id ON task_comment(task_id);

CREATE INDEX ix_task_label_label_id ON task_label(label_id);
