export interface Todo {
    id: string;
    title: string;
    description: string;
    isCompleted: boolean;
}

export interface ApiResponse<T> {
    success: boolean;
    data: T | null;
    message: string;
    errors: string[] | null;
}

export interface CreateTodo {
    title: string;
    description: string;
}