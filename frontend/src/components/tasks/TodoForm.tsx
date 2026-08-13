import {
  Card,
  Field,
  Input,
  Textarea,
  Dropdown,
  Option,
} from "@fluentui/react-components";

import { Controller } from "react-hook-form";
import type { Control, FieldErrors } from "react-hook-form";

import type { CreateTodo } from "../../models/Todo";
import { TodoStatus } from "../../enums/TodoStatus";

interface TodoFormProps {
  control: Control<CreateTodo>;
  errors: FieldErrors<CreateTodo>;
}

function TodoForm({ control, errors }: TodoFormProps) {
  return (
    <Card style={{ gap: "16px" }}>
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

      <Field label="Status">
        <Controller
          name="status"
          control={control}
          render={({ field }) => (
            <Dropdown
              value={field.value}
              selectedOptions={[field.value]}
              onOptionSelect={(_, data) => {
                if (data.optionValue) {
                  field.onChange(data.optionValue as TodoStatus);
                }
              }}
            >
              <Option value={TodoStatus.Pending}>{TodoStatus.Pending}</Option>
              <Option value={TodoStatus.Completed}>{TodoStatus.Completed}</Option>
              <Option value={TodoStatus.Done}>{TodoStatus.Done}</Option>
            </Dropdown>
          )}
        />
      </Field>
    </Card>
  );
}

export default TodoForm;
