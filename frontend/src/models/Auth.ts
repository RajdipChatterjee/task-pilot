export interface User {
    id: string;
    username: string;
    email: string;
}

export interface LoginDto {
    usernameOrEmail: string;
    password: string;
}

export interface RegisterDto {
    username: string;
    email: string;
    password: string;
}