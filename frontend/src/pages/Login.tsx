import { useState } from "react";
import { useDispatch } from "react-redux";
import {
    Button,
    Field,
    Input,
    makeStyles,
    Title3,
} from "@fluentui/react-components";

import { login, getCurrentUser } from "../api/authApi";
import { setUser } from "../features/auth/authSlice";
import type { AppDispatch } from "../store/store";

const useStyles = makeStyles({
    container: {
        width: "400px",
        margin: "100px auto",
        display: "flex",
        flexDirection: "column",
        gap: "16px",
    },
});

export default function Login() {
    const styles = useStyles();
    const dispatch = useDispatch<AppDispatch>();

    const [usernameOrEmail, setUsernameOrEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    async function handleSubmit(e: React.FormEvent) {
        e.preventDefault();
        setError("");

        try {
            // Backend sets accessToken + refreshToken cookies
            await login({
                usernameOrEmail,
                password,
            });

            // Ask backend who is currently authenticated
            const user = await getCurrentUser();

            // Store only user/application state in Redux
            dispatch(setUser(user));

            console.log("Logged in successfully");
        } catch (error) {
            setError("Invalid username/email or password.");
        }
    }

    return (
        <form className={styles.container} onSubmit={handleSubmit}>
            <Title3>Login to TaskPilot</Title3>

            <Field label="Username or Email">
                <Input
                    value={usernameOrEmail}
                    onChange={(_, data) =>
                        setUsernameOrEmail(data.value)
                    }
                />
            </Field>

            <Field label="Password">
                <Input
                    type="password"
                    value={password}
                    onChange={(_, data) =>
                        setPassword(data.value)
                    }
                />
            </Field>

            {error && <div>{error}</div>}

            <Button appearance="primary" type="submit">
                Login
            </Button>
        </form>
    );
}