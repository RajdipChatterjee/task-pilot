import { useState } from "react";

import {
  Button,
  Card,
  CardHeader,
  Field,
  Input,
  Text,
  Textarea,
} from "@fluentui/react-components";

import type { CreateTodo } from "../models/Todo";

interface TodoFormProps {
  handleCreateTodo: (todo: CreateTodo) => Promise<void>;
}

function TodoForm({ handleCreateTodo }: TodoFormProps) {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");

  async function handleSubmit() {

    if (!title.trim()) return;

    const todo: CreateTodo = {
      title: title,
      description: description,
    };

    await handleCreateTodo(todo);

    setTitle("");
    setDescription("");
  }

  return (
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

      </div>
      <div style={{ marginTop: 20 }}>
        <Button appearance="primary" onClick={handleSubmit}>
          Add Todo
        </Button>
      </div>
    </Card>
  );
}

export default TodoForm;
