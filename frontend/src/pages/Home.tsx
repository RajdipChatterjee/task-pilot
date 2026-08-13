import { useState, useEffect } from "react";

import * as todoApi from "../api/todoApi";
import type { Todo } from "../models/Todo";
import TodoList from "../components/tasks/TodoList";
import ToolBar from "../components/tasks/ToolBar";

function Home() {
  const [todos, setTodos] = useState<Todo[]>([]);

  async function loadTodos() {
    const data = await todoApi.getTodos();
    setTodos(data);
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
      <ToolBar loadTodos={loadTodos} />
      <TodoList
        todos={todos}
        deleteTodo={handleDeleteTodo}
        loadTodos={loadTodos}
      />
    </div>
  );
}

export default Home;
