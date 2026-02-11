-- =============================================
-- ToDo List API – seed data (run after 002_indexes.sql)
-- =============================================

-- Labels
INSERT INTO label (id, name, color, created_at) VALUES
  ('a0000001-0000-0000-0000-000000000001'::uuid, 'Important', '#000000', now()), -- Important
  ('a0000001-0000-0000-0000-000000000002'::uuid, 'Spam', '#dc2626', now()), -- Spam
  ('a0000001-0000-0000-0000-000000000003'::uuid, 'Investigate', '#ea580c', now()) -- Investigate
ON CONFLICT (id) DO NOTHING;

-- Tasks 
INSERT INTO task (id, title, description, status, priority, due_at, created_at, updated_at, completed_at) VALUES
  ('b0000001-0000-0000-0000-000000000001'::uuid, 'Task 1 - no labels', 'First task.', 'todo', 'medium', NULL, now(), now(), NULL),
  ('b0000001-0000-0000-0000-000000000002'::uuid, 'Task 2 - Important & Investigate', 'Second task.', 'in_progress', 'high', NULL, now(), now(), NULL),
  ('b0000001-0000-0000-0000-000000000003'::uuid, 'Task 3 - Spam', 'Third task.', 'todo', 'low', NULL, now(), now(), NULL),
  ('b0000001-0000-0000-0000-000000000004'::uuid, 'Task 4 - no labels', 'Fourth task.', 'todo', 'medium', NULL, now(), now(), NULL)
ON CONFLICT (id) DO NOTHING;

-- Task–Label links 
INSERT INTO task_label (task_id, label_id) VALUES
  ('b0000001-0000-0000-0000-000000000002'::uuid, 'a0000001-0000-0000-0000-000000000001'::uuid), -- Task 2: Important
  ('b0000001-0000-0000-0000-000000000002'::uuid, 'a0000001-0000-0000-0000-000000000003'::uuid), -- Task 2: Investigate
  ('b0000001-0000-0000-0000-000000000003'::uuid, 'a0000001-0000-0000-0000-000000000002'::uuid)  -- Task 3: Spam
ON CONFLICT (task_id, label_id) DO NOTHING;
