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
  MailRegular,
  PersonColor,
} from "@fluentui/react-icons";
import { makeStyles } from "@fluentui/react-components";
import { useForm } from "react-hook-form";
import google from "../assets/chrome.svg";
import github from "../assets/github.svg";
import { useState } from "react";
import { zodResolver } from "@hookform/resolvers/zod";
import { registerSchema, type RegisterFormData } from "../schemas/auth.schema";
import type { RegisterDto } from "../models/Auth";
import { registerUser } from "../api/authApi";

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
  createAccountButton: {
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

function Register() {
  const styles = useStyles();
  const navigate = useNavigate();

  const [isLoading, setIsLoading] = useState(false);

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<RegisterFormData>({
    resolver: zodResolver(registerSchema),
  });

  async function onSubmit(data: RegisterFormData) {
    setIsLoading(true);

    try {
      const payload: RegisterDto = {
        username: data.username,
        email: data.email,
        password: data.password,
      };
      const result = await registerUser(payload);
      navigate("/login");
      console.log(result);
    } catch (error) {
      console.log(error);
    } finally {
      setIsLoading(false);
    }
  }

  const [passwordVisible, setPasswordVisible] = useState(false);
  const [confirmPasswordVisible, setConfirmPasswordVisible] = useState(false);

  function togglePasswordVisibility() {
    setPasswordVisible((prev) => !prev);
  }

  function toggleConfirmPasswordVisibility() {
    setConfirmPasswordVisible((prev) => !prev);
  }

  return (
    <Card className={styles.card}>
      <form className={styles.form} onSubmit={handleSubmit(onSubmit)}>
        <CardHeader
          header={
            <div>
              <Title1>
                <b>Create your workspace</b>
              </Title1>
              <br />
              <Body1>Start organizing in minutes</Body1>
            </div>
          }
          style={{ marginBottom: "14px" }}
        />

        <Field
          label="Username"
          className={styles.field}
          validationState={errors.username ? "error" : "none"}
          validationMessage={errors.username?.message}
        >
          <Input
            {...register("username")}
            className={styles.input}
            placeholder="Username"
            contentBefore={<PersonColor />}
          />
        </Field>

        <Field
          label="Email"
          className={styles.field}
          validationState={errors.email ? "error" : "none"}
          validationMessage={errors.email?.message}
        >
          <Input
            {...register("email")}
            className={styles.input}
            placeholder="design@studio.com"
            contentBefore={<MailRegular />}
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

        <Field
          label="Confirm Password"
          className={styles.field}
          validationState={errors.confirmPassword ? "error" : "none"}
          validationMessage={errors.confirmPassword?.message}
        >
          <Input
            {...register("confirmPassword")}
            className={styles.input}
            type={confirmPasswordVisible ? "text" : "password"}
            placeholder="password"
            contentBefore={<LockClosedFilled />}
            contentAfter={
              <Button
                type="button"
                icon={
                  confirmPasswordVisible ? <EyeRegular /> : <EyeOffRegular />
                }
                appearance="transparent"
                onClick={toggleConfirmPasswordVisibility}
              />
            }
          />
        </Field>
        <Button
          type="submit"
          shape="rounded"
          className={styles.createAccountButton}
          size="large"
          disabled={isLoading}
        >
          {isLoading ? (
            <Spinner labelPosition="below" label="Creating Account..." />
          ) : (
            "Create Account"
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
          Already have an account?{" "}
          <Link style={{ fontWeight: "bold", color: "#4F46E5" }} to="/login">
            Sign in
          </Link>
        </Body1>
      </form>
    </Card>
  );
}

export default Register;
