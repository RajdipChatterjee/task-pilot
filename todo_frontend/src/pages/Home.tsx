import { useState, useEffect } from "react";

import * as todoApi from "../api/todoApi";
import type { Todo } from "../models/Todo";
import TodoList from "../components/TodoList";
import ToolBar from "../components/ToolBar";

function Home() {
  const [todos, setTodos] = useState<Todo[]>([]);

  async function loadTodos() {
    const data = await todoApi.getTodos();
    setTodos(data);
  }

  async function handleToggleStatus(id: string) {
    const todo = todos.find((todo) => todo.id == id);
    if (!todo) return;

    const updatedTodo = { ...todo, isCompleted: !todo?.isCompleted };

    await todoApi.updateTodo(id, updatedTodo);
    await loadTodos();
  }

  async function handleDeleteTodo(id: string) {
    await todoApi.deleteTodo(id);
    await loadTodos();
  }

  useEffect(() => {
    void loadTodos();
  }, []);

  return (
    <div
      style={{
        maxWidth: "800px",
        margin: "40px auto",
        padding: "20px",
      }}
    >
      <ToolBar />
      <TodoList
        todos={todos}
        toggleStatus={handleToggleStatus}
        deleteTodo={handleDeleteTodo}
      />
    </div>
  );
}

export default Home;
