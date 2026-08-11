import {createSlice, type PayloadAction} from "@reduxjs/toolkit";
import type { User } from "../../models/Auth";

interface AuthState {
    user: User | null
    isAuthenticated: boolean
}

const initialState : AuthState = {
    user: null,
    isAuthenticated: false
}

const authSlice = createSlice({
    name: "auth",
    initialState,

    // reducers must be synchronous
    reducers: {
        setUser : (state, action : PayloadAction<User>) => {
            state.user = action.payload;
            state.isAuthenticated = true;
        },

        logout: (state) => {
            state.user = null;
            state.isAuthenticated = false;
        }
    }
});

export default authSlice.reducer;

export const {setUser, logout} = authSlice.actions;