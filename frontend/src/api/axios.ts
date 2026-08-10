import axios from "axios";
import type { AxiosInstance } from "axios";

const api: AxiosInstance = axios.create({
    baseURL: "https://localhost:7127/api",
    headers: {
        "Content-Type": "application/json",
    },
    withCredentials: true,
});

export default api;