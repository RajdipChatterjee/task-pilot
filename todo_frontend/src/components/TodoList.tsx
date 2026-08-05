import { Text } from "@fluentui/react-components";
import TodoCard from "./TodoCard";
import type { Todo } from "../models/Todo";

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
        <TodoCard
          key={todo.id}
          todo={todo}
          handleToggleStatus={() => handleToggleStatus(todo.id)}
          handleDeleteClick={() => handleDeleteClick(todo.id)}
        />
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
