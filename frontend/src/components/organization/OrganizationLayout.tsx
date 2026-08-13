import { useSelector } from "react-redux";
import { makeStyles } from "@fluentui/react-components";
import type { ReactNode } from "react";
import type { RootState } from "../../store/store";
import {
  Avatar,
  Text,
} from "@fluentui/react-components";

interface OrganizationLayoutProps {
  children: ReactNode;
}

const useStyles = makeStyles({
  page: {
    minHeight: "100vh",
    backgroundColor: "#F8FAFC",
  },

  header: {
    height: "60px",
    display: "flex",
    alignItems: "center",
    justifyContent: "space-between",
    padding: "0 32px",
    backgroundColor: "white",
    borderBottom: "1px solid #E5E7EB",
  },

  logo: {
    fontWeight: 700,
    fontSize: "18px",
  },

  userEmail: {
    fontSize: "13px",
    color: "#64748B",
  },

  content: {
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
    minHeight: "calc(100vh - 60px)",
    padding: "40px 20px",
  },
});

function OrganizationLayout({ children }: OrganizationLayoutProps) {
  const styles = useStyles();

  const user = useSelector((state: RootState) => state.auth.user);

  return (
    <div className={styles.page}>
      <header className={styles.header}>
        <Text weight="semibold">TaskPilot</Text>

        <div className={styles.user}>
          <Avatar name={user?.username} size={24} />
          <Text size={200}>{user?.email}</Text>
        </div>
      </header>

      <main className={styles.content}>{children}</main>
    </div>
  );
}

export default OrganizationLayout;
