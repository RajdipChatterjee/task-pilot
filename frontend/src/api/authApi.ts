import api from "./axios";

import type { LoginDto, RegisterDto, User } from "../models/Auth";

import type { ApiResponse } from "../models/ApiResponse";

export async function loginUser(dto: LoginDto) {
  const response = await api.post<ApiResponse<string>>("/auth/login", dto);

  if (!response.data.success) throw new Error(response.data.message);

  return response.data;
}

export async function refresh() {
  const response = await api.post<ApiResponse<string>>("/auth/refresh");

  if (!response.data.success) throw new Error(response.data.message);

  return response.data;
}

export async function registerUser(dto: RegisterDto) {
  const response = await api.post<ApiResponse<string>>("/auth/register", dto);

  if (!response.data.success) throw new Error(response.data.message);

  return response.data;
}

export async function getCurrentUser(): Promise<User> {
  const response = await api.get<ApiResponse<User>>("/auth/me");

  if (!response.data.success) throw new Error(response.data.message);
  if (!response.data.data) throw new Error("User data not found");
  return response.data.data;
}
