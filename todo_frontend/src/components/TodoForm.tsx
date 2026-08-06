import {
  Card,
  CardHeader,
  Field,
  Input,
  Text,
  Textarea,
} from "@fluentui/react-components";

import { Controller } from "react-hook-form";
import type { Control, FieldErrors } from "react-hook-form";

import type { CreateTodo } from "../models/Todo";

interface TodoFormProps {
  control: Control<CreateTodo>;
  errors: FieldErrors<CreateTodo>;
}

function TodoForm({ control, errors }: TodoFormProps) {
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

      <Field
        label="Title"
        validationState={errors.title ? "warning" : "none"}
        validationMessage={errors.title?.message}
      >
        <Controller
          name="title"
          control={control}
          rules={{
            required: "Title is required",
          }}
          render={({ field }) => <Input {...field} />}
        />
      </Field>

      <Field label="Description">
        <Controller
          name="description"
          control={control}
          render={({ field }) => <Textarea {...field} />}
        />
      </Field>
    </Card>
  );
}

export default TodoForm;
