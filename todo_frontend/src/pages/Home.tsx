import { useState, useEffect } from "react";

import {
  Button,
  Card,
  CardHeader,
  Field,
  Input,
  Text,
  Textarea,
} from "@fluentui/react-components";

import * as todoApi from "../api/todoApi";
import type { Todo, CreateTodo } from "../models/Todo";

function Home() {
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
    <div
      style={{
        maxWidth: "800px",
        margin: "40px auto",
        padding: "20px",
      }}
    >
      <Card>
        <CardHeader
          header={
            <Text size={700} weight="semibold">
              Todo Application
            </Text>
          }
          description="Built with React + ASP.NET Core"
        />

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

          {/* <Button appearance="primary">Add Todo</Button> */}
        </div>
        <div style={{ marginTop: 20 }}>
          <Button appearance="primary" onClick={handleAddTodo}>
            Add Todo
          </Button>
        </div>
      </Card>
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

                await loadTodos();
              }}
            >
              {todo.isCompleted ? "Mark Pending" : "Mark Complete"}
            </Button>
            <Button
              appearance="secondary"
              onClick={async () => {
                await todoApi.deleteTodo(todo.id);
                await loadTodos();
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

export default Home;
