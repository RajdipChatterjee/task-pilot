import axios from "axios";
import type { AxiosInstance } from "axios";

import { store } from "../store/store";

const api: AxiosInstance = axios.create({
    baseURL: "https://localhost:7127/api",
    headers: {
        "Content-Type": "application/json",
    },
});

api.interceptors.request.use((config) => {
    const accessToken = store.getState().auth.accessToken;

    if (accessToken) {
        config.headers.Authorization = `Bearer ${accessToken}`;
    }

    return config;
});

export default api;