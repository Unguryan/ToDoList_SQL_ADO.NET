-- =============================================
-- ToDo List API – Initial schema (types + tables)
-- PostgreSQL, run once per environment
-- =============================================

-- Enum types (match application enums)
CREATE TYPE task_status AS ENUM ('todo', 'in_progress', 'done', 'canceled');
CREATE TYPE task_priority AS ENUM ('low', 'medium', 'high', 'critical');

-- Tasks
CREATE TABLE task (
  id uuid PRIMARY KEY,
  title text NOT NULL,
  description text NULL,
  status task_status NOT NULL DEFAULT 'todo',
  priority task_priority NOT NULL DEFAULT 'medium',
  due_at timestamptz NULL,
  created_at timestamptz NOT NULL DEFAULT now(),
  updated_at timestamptz NOT NULL DEFAULT now(),
  completed_at timestamptz NULL,

  CONSTRAINT ck_task_completed_at
    CHECK (
      (status = 'done' AND completed_at IS NOT NULL)
      OR
      (status <> 'done' AND completed_at IS NULL)
    )
);

-- Comments on tasks (1:M)
CREATE TABLE task_comment (
  id uuid PRIMARY KEY,
  task_id uuid NOT NULL REFERENCES task(id) ON DELETE CASCADE,
  body text NOT NULL,
  created_at timestamptz NOT NULL DEFAULT now()
);

-- Labels (reusable tags)
CREATE TABLE label (
  id uuid PRIMARY KEY,
  name text NOT NULL UNIQUE,
  color text NULL,
  created_at timestamptz NOT NULL DEFAULT now()
);

-- Task–Label (M:M)
CREATE TABLE task_label (
  task_id uuid NOT NULL REFERENCES task(id) ON DELETE CASCADE,
  label_id uuid NOT NULL REFERENCES label(id) ON DELETE CASCADE,
  PRIMARY KEY (task_id, label_id)
);
