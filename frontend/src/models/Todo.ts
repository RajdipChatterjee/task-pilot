import { TodoStatus } from "../enums/TodoStatus";

export interface Todo {
  id: string;
  title: string;
  description: string;
  status: TodoStatus;
}

export interface CreateTodo {
  title: string;
  description?: string;
  status: TodoStatus;
}

export interface UpdateTodo {
  title: string;
  description?: string;
  status: TodoStatus;
}