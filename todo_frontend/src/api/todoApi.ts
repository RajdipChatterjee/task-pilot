import api from "./axios";
import type { Todo, CreateTodo } from "../models/Todo.tsx";

export async function createTodo(todo: CreateTodo) {
    const response = await api.post<Todo>("/todo", todo);
    return response.data;
}

export async function getTodos() {
    const response = await api.get<Todo[]>("/todo");
    return response.data;
}

export async function getTodo(id: string) {
    const response = await api.get<Todo>(`/todo/${id}`);
    return response.data;
}

 