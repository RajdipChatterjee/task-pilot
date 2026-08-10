import api from "./axios";

export interface LoginDto {
    usernameOrEmail: string;
    password: string;
}

export interface RegisterDto {
    username: string;
    email: string;
    password: string;
}

export interface AuthResponse {
    accessToken: string;
    refreshToken: string;
}

export interface User {
    id: string;
    username: string;
}

export async function login(dto: LoginDto) {
    const response = await api.post("/auth/login", dto);

    return response.data.data as AuthResponse;
}

export async function register(dto: RegisterDto) {
    const response = await api.post("/auth/register", dto);

    return response.data;
}

export async function getCurrentUser() {
    const response = await api.get("/auth/me");

    return response.data;
}

export async function refreshToken(refreshToken: string) {
    const response = await api.post(
        "/auth/refresh",
        JSON.stringify(refreshToken),
        {
            headers: {
                "Content-Type": "application/json",
            },
        }
    );

    return response.data.data as AuthResponse;
}