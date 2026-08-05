import type { Todo } from "../models/Todo";

import { Card, CardHeader, Text, Button } from "@fluentui/react-components";

interface TodoListProps {
  todos: Todo[];
  toggleStatus: (id: string) => Promise<void>;
  deleteTodo: (id: string) => Promise<void>;
}

function TodoList({ todos, toggleStatus, deleteTodo }: TodoListProps) {
  async function handleDeleteClick(id: string) {
    await deleteTodo(id);
  }

  async function handleToggleStatus(id: string) {
    await toggleStatus(id);
  }

  return (
    <div>
      {todos.map((todo) => (
        <Card key={todo.id} style={{ marginTop: 16, padding: 16 }}>
          <CardHeader
            header={<Text weight="semibold">{todo.title}</Text>}
            description={
              <div>
                <Text>{todo.description}</Text>

                <br />

                <Text size={200}>
                  {todo.isCompleted ? "✅ Completed" : "⏳ Pending"}
                </Text>
              </div>
            }
          />
          <div
            style={{
              marginTop: 16,
              display: "flex",
              justifyContent: "space-between",
            }}
          >
            <Button
              appearance="primary"
              onClick={async () => handleToggleStatus(todo.id)}
            >
              {todo.isCompleted ? "Mark Pending" : "Mark Complete"}
            </Button>

            <Button
              appearance="secondary"
              onClick={async () => handleDeleteClick(todo.id)}
            >
              Delete
            </Button>
          </div>
        </Card>
      ))}
      {todos.length === 0 && (
        <Text
          italic
          style={{
            marginTop: 24,
            display: "block",
            textAlign: "center",
          }}
        >
          No todos yet.
        </Text>
      )}
    </div>
  );
}

export default TodoList;
