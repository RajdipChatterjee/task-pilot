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
import { useEffect } from "react";

interface TaskDialogProps {
  mode: DialogMode;
  todo?: Todo;
  onSuccess?: () => void;
}

function TaskDialog({ mode, todo, onSuccess }: TaskDialogProps) {
  const {
    control,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<CreateTodo>({
    defaultValues: {
      title: "",
      description: "",
    },
  });

  async function onSubmit(todoData: CreateTodo) {
    if (mode === DialogMode.Create) {
      await todoApi.createTodo(todoData);
    } else {
      await todoApi.updateTodo(todo!.id, todoData);
    }

    onSuccess?.();
  }

  useEffect(() => {
    if (mode === DialogMode.Edit && todo) {
      reset({
        title: todo.title,
        description: todo.description,
      });
    } else {
      reset({
        title: "",
        description: "",
      });
    }
  }, [mode, todo, reset]);

  return (
    <Dialog>
      <DialogTrigger>
        <Button
          icon={
            mode == DialogMode.Create ? (
              <CollectionsAddRegular />
            ) : (
              <EditRegular />
            )
          }
        >
          {mode == DialogMode.Create ? "Add Task" : ""}
        </Button>
      </DialogTrigger>
      <DialogSurface>
        <DialogBody>
          <DialogTitle>Task Details</DialogTitle>
          <DialogContent>
            <TodoForm control={control} errors={errors} />
          </DialogContent>

          <DialogActions fluid={true}>
            <DialogTrigger disableButtonEnhancement>
              <Button appearance="primary" onClick={handleSubmit(onSubmit)}>
                {mode == DialogMode.Create ? "Add Task" : "Save"}
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
