import api from "./axios";

import type {
    LoginDto,
    RegisterDto,
    User
} from "../models/Auth";

import type { ApiResponse } from "../models/ApiResponse";

export async function login(dto: LoginDto) {
    const response = await api.post<ApiResponse<string>>(
        "/auth/login",
        dto
    );

    if (!response.data.success)
        throw new Error(response.data.message);

    return response.data;
}

export async function refresh() {
    const response = await api.post<ApiResponse<string>>(
        "/auth/refresh"
    );

    if (!response.data.success)
        throw new Error(response.data.message);

    return response.data;
}

export async function register(dto: RegisterDto) {
    const response = await api.post<ApiResponse<string>>(
        "/auth/register",
        dto
    );

    if (!response.data.success)
        throw new Error(response.data.message);

    return response.data;
}

export async function getCurrentUser() {
    const response = await api.get<ApiResponse<User>>("/auth/me");

    if (!response.data.success)
    throw new Error(response.data.message);

    return response.data.data;
}