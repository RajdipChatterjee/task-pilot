import api from "./axios";
import type { ApiResponse, CreateTodo, UpdateTodo, Todo } from "../models/Todo";

export async function createTodo(todo: CreateTodo) {
    const response = await api.post<ApiResponse<Todo>>("/todo", todo);

    if (!response.data.data)
        throw new Error(response.data.message);

    return response.data.data;
}

export async function getTodos() {
    const response = await api.get<ApiResponse<Todo[]>>("/todo");
    return response.data.data ?? [];
}

export async function getTodo(id: string) {
    const response = await api.get<ApiResponse<Todo>>(`/todo/${id}`);
    return response.data.data;
}

export async function updateTodo(id: string, todo: UpdateTodo) {
    await api.put(`/todo/${id}`, todo);
}

export async function deleteTodo(id: string) {
    await api.delete(`/todo/${id}`);
}