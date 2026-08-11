import {
  Body1,
  Button,
  Card,
  CardFooter,
  CardHeader,
  Input,
  Label,
  Link,
  Title1,
} from "@fluentui/react-components";
import {
  EyeOffRegular,
  EyeRegular,
  LockClosedFilled,
  PersonColor,
} from "@fluentui/react-icons";
import { makeStyles } from "@fluentui/react-components";
import { useForm } from "react-hook-form";
import type { LoginDto } from "../models/Auth";
import { useState } from "react";

const useStyles = makeStyles({
  rightElement: {
    marginLeft: "auto",
  },
  makeSpace:{
    padding: "4.5%"
  }
});

function Login() {
  const styles = useStyles();

  const { register, handleSubmit } = useForm<LoginDto>();
  function onSubmit(data: LoginDto) {
    console.log(data)
  }

  const [passwordVisible, setPasswordVisible] = useState(false);
  function togglePasswordVisibility() {
    setPasswordVisible((prev) => !prev);
  }

  return (
    <Card className={styles.makeSpace}>
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
      />
      <Label>Username or Email</Label>
      <Input
        {...register("usernameOrEmail")}
        type="email"
        placeholder="hello@taskpilot.com"
        contentBefore={<PersonColor />}
      />
      <Label>Password</Label>
      <Input
        {...register("password")}
        className=""
        type={passwordVisible ? "text" : "password"}
        placeholder="password"
        contentBefore={<LockClosedFilled />}
        contentAfter={
          <Button
            icon={passwordVisible ? <EyeRegular /> : <EyeOffRegular />}
            appearance="transparent"
            onClick={togglePasswordVisibility}
          />
        }
      />
      <Link href="https://www.bing.com" className={styles.rightElement}>
        Forgot password?
      </Link>
      <CardFooter>
        <Button type="submit" shape="rounded" onSubmit={handleSubmit(onSubmit)}>
          Sign In
        </Button>
      </CardFooter>
    </Card>
  );
}

export default Login;
