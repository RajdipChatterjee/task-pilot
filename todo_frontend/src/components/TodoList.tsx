import type { Todo } from "../models/Todo";

import { Card, CardHeader, Text, Button } from "@fluentui/react-components";

import * as todoApi from "../api/todoApi";

interface TodoListProps {
    todos: Todo[],
    loadTodos: () => Promise<void>
}

function TodoList({todos, loadTodos}: TodoListProps) {
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
              onClick={async () => {
                await todoApi.updateTodo(todo.id, {
                  ...todo,
                  isCompleted: !todo.isCompleted,
                });

                loadTodos();
              }}
            >
              {todo.isCompleted ? "Mark Pending" : "Mark Complete"}
            </Button>
            <Button
              appearance="secondary"
              onClick={async () => {
                await todoApi.deleteTodo(todo.id);

                loadTodos();
              }}
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
