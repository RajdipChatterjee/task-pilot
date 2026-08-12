import {
  Body1,
  Button,
  Card,
  CardFooter,
  CardHeader,
  Divider,
  Field,
  Input,
  Spinner,
  Title1,
} from "@fluentui/react-components";
import { Link, useNavigate } from "react-router-dom";
import {
  EyeOffRegular,
  EyeRegular,
  LockClosedFilled,
  PersonColor,
} from "@fluentui/react-icons";
import { makeStyles } from "@fluentui/react-components";
import { useForm } from "react-hook-form";
import google from "../assets/chrome.svg";
import github from "../assets/github.svg";
import { useState } from "react";
import { zodResolver } from "@hookform/resolvers/zod";
import { loginSchema, type LoginFormData } from "../schemas/auth.schema";
import type { LoginDto } from "../models/Auth";
import { getCurrentUser, loginUser } from "../api/authApi";
import { useDispatch } from "react-redux";
import type { AppDispatch } from "../store/store";
import { setUser } from "../features/auth/authSlice";

const useStyles = makeStyles({
  rightElement: {
    marginLeft: "auto",
  },
  card: {
    padding: "4.5%",
    borderRadius: "24px",
  },
  form: {
    display: "flex",
    flexDirection: "column",
    width: "100%",
  },
  input: {
    borderRadius: "12px",
    height: "48px",
    backgroundColor: "#F8FAFC",
  },
  field: {
    marginTop: "10px",
  },
  signinButton: {
    width: "100%",
    color: "white",
    background: "#4F46E5",
    borderRadius: "12px",
    height: "55px",
    marginTop: "14px",
    marginBottom: "14px",
  },
  oAuthButtons: {
    flexGrow: 1,
    padding: "12px",
    borderRadius: "10px",
    display: "flex",
    gap: "6px",
  },
});

function Login() {
  const styles = useStyles();
  const navigate = useNavigate();
  const dispatch = useDispatch<AppDispatch>();

  const [isLoading, setIsLoading] = useState(false);

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginFormData>({
    resolver: zodResolver(loginSchema),
  });

  async function onSubmit(data: LoginFormData) {
    setIsLoading(true);

    try {
      const payload: LoginDto = {
        usernameOrEmail: data.usernameOrEmail,
        password: data.password,
      };
      const result = await loginUser(payload);

      const user = await getCurrentUser();

      dispatch(setUser(user));

      navigate("/", { replace: true });
      console.log(result);
    } catch (error) {
      console.log(error);
    } finally {
      setIsLoading(false);
    }
  }

  const [passwordVisible, setPasswordVisible] = useState(false);

  function togglePasswordVisibility() {
    setPasswordVisible((prev) => !prev);
  }

  return (
    <Card className={styles.card}>
      <form className={styles.form} onSubmit={handleSubmit(onSubmit)}>
        <CardHeader
          header={
            <div>
              <Title1>
                <b>Welcome back</b>
              </Title1>
              <br />
              <Body1>Sign in to your workspace</Body1>
            </div>
          }
          style={{ marginBottom: "14px" }}
        />
        <Field
          label="Username or Email"
          className={styles.field}
          validationState={errors.usernameOrEmail ? "error" : "none"}
          validationMessage={errors.usernameOrEmail?.message}
        >
          <Input
            {...register("usernameOrEmail")}
            className={styles.input}
            placeholder="hello@taskpilot.com"
            contentBefore={<PersonColor />}
          />
        </Field>
        <Field
          label="Password"
          className={styles.field}
          validationState={errors.password ? "error" : "none"}
          validationMessage={errors.password?.message}
        >
          <Input
            {...register("password")}
            className={styles.input}
            type={passwordVisible ? "text" : "password"}
            placeholder="password"
            contentBefore={<LockClosedFilled />}
            contentAfter={
              <Button
                type="button"
                icon={passwordVisible ? <EyeRegular /> : <EyeOffRegular />}
                appearance="transparent"
                onClick={togglePasswordVisibility}
              />
            }
          />
        </Field>
        <Link to="" className={styles.rightElement}>
          Forgot password?
        </Link>
        <Button
          type="submit"
          shape="rounded"
          className={styles.signinButton}
          size="large"
        >
          {isLoading ? (
            <Spinner labelPosition="below" label="Signing In..." />
          ) : (
            "Sign In"
          )}
        </Button>
        <Divider style={{ marginTop: 14, marginBottom: 14 }}>
          {" "}
          OR CONTINUE WITH{" "}
        </Divider>
        <CardFooter
          style={{ display: "flex", marginTop: 14, marginBottom: 14 }}
        >
          <Button type="button" className={styles.oAuthButtons}>
            <img src={google} alt="" />
            Google
          </Button>
          <Button type="button" className={styles.oAuthButtons}>
            <img src={github} alt="" />
            GitHub
          </Button>
        </CardFooter>
        <Body1 style={{ margin: "auto" }}>
          Don't have an account?{" "}
          <Link style={{ fontWeight: "bold", color: "#4F46E5" }} to="/register">
            Create one
          </Link>
        </Body1>
      </form>
    </Card>
  );
}

export default Login;
