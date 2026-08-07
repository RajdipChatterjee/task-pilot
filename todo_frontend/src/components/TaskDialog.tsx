import {
  Dialog,
  DialogTrigger,
  Button,
  DialogSurface,
  DialogBody,
  DialogTitle,
  DialogContent,
  DialogActions,
} from "@fluentui/react-components";
import { CollectionsAddRegular, EditRegular } from "@fluentui/react-icons";
import { useForm } from "react-hook-form";
import TodoForm from "./TodoForm";
import * as todoApi from "../api/todoApi";
import type { CreateTodo, Todo } from "../models/Todo";
import { DialogMode } from "../enums/DialogMode";
import { TodoStatus } from "../enums/TodoStatus";
import { useEffect, useState } from "react";

interface TaskDialogProps {
  mode: DialogMode;
  todo?: Todo;
  onSuccess: () => void;
}

function TaskDialog({ mode, todo, onSuccess }: TaskDialogProps) {
  const [open, setOpen] = useState(false);

  const {
    control,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<CreateTodo>({
    defaultValues: {
      title: "",
      description: "",
      status: TodoStatus.Pending,
    },
  });

  async function onSubmit(todoData: CreateTodo) {
    if (mode === DialogMode.Create) {
      await todoApi.createTodo(todoData);
    } else {
      await todoApi.updateTodo(todo!.id, todoData);
    }

    await onSuccess();
    reset();
    setOpen(false);
  }

  useEffect(() => {
    if (mode === DialogMode.Edit && todo) {
      reset({
        title: todo.title,
        description: todo.description,
        status: todo.status ?? TodoStatus.Pending,
      });
    } else {
      reset({
        title: "",
        description: "",
        status: TodoStatus.Pending,
      });
    }
  }, [mode, todo, reset]);

  return (
    <Dialog
      open={open}
      onOpenChange={(_, data) => setOpen(data.open)}
    >
      <DialogTrigger disableButtonEnhancement>
        <Button
          icon={
            mode === DialogMode.Create ? (
              <CollectionsAddRegular />
            ) : (
              <EditRegular />
            )
          }
        >
          {mode === DialogMode.Create ? "Add Task" : ""}
        </Button>
      </DialogTrigger>
      <DialogSurface>
        <DialogBody>
          <DialogTitle>
            {mode === DialogMode.Create ? "Create Task" : "Edit Task"}
          </DialogTitle>
          <DialogContent>
            <TodoForm control={control} errors={errors} />
          </DialogContent>

          <DialogActions fluid={true}>
            <DialogTrigger disableButtonEnhancement>
              <Button appearance="primary" onClick={handleSubmit(onSubmit)}>
                {mode === DialogMode.Create ? "Add Task" : "Save"}
              </Button>
            </DialogTrigger>
            <DialogTrigger disableButtonEnhancement>
              <Button appearance="secondary">Cancel</Button>
            </DialogTrigger>
          </DialogActions>
        </DialogBody>
      </DialogSurface>
    </Dialog>
  );
}

export default TaskDialog;
