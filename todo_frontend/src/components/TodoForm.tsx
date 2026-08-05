import { Field, Input, Textarea, Button } from "@fluentui/react-components";
import { useState, useEffect } from "react";
import type { Todo, CreateTodo } from "../models/Todo";
import * as todoApi from "../api/todoApi";

function TodoForm() {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [todos, setTodos] = useState<Todo[]>([]);

  async function handleAddTodo() {
    const todo: CreateTodo = {
      title,
      description,
    };

    await todoApi.createTodo(todo);

    setTitle("");
    setDescription("");

    await loadTodos();
  }

  async function loadTodos() {
    const data = await todoApi.getTodos();
    setTodos(data);
  }

  useEffect(() => {
    void loadTodos();
  }, []);

  return (
    <div>
      <div
        style={{
          display: "flex",
          flexDirection: "column",
          gap: 16,
          marginTop: 20,
        }}
      >
        <Field
          label="Title"
          validationState={title ? "none" : "warning"}
          validationMessage={title ? "" : "Title is required"}
        >
          <Input value={title} onChange={(_, data) => setTitle(data.value)} />
        </Field>

        <Field label="Description">
          <Textarea
            value={description}
            onChange={(_, data) => setDescription(data.value)}
          />
        </Field>

      </div>
      <div style={{ marginTop: 20 }}>
        <Button appearance="primary" onClick={handleAddTodo}>
          Add Todo
        </Button>
      </div>
    </div>
  );
}

export default TodoForm;
