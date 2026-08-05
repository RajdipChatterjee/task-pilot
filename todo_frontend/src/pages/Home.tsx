import { useState, useEffect } from "react";

import * as todoApi from "../api/todoApi";
import type { Todo, CreateTodo } from "../models/Todo";
import TodoForm from "../components/TodoForm";
import TodoList from "../components/TodoList";

function Home() {
  const [todos, setTodos] = useState<Todo[]>([]);

  async function loadTodos() {
    const data = await todoApi.getTodos();
    setTodos(data);
  }

  async function handleCreateTodo(todo: CreateTodo) {
    await todoApi.createTodo(todo);

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
      
      <TodoForm handleCreateTodo={handleCreateTodo}/>
      <TodoList todos={todos} loadTodos={loadTodos}/>
    </div>
  );
}

export default Home;
