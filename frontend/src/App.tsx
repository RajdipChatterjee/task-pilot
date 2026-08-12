import Home from "./pages/Home";
import { Route, Routes } from "react-router-dom";
import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/RegisterPage";
import ProtectedRoute from "./routes/ProtectedRoute";
import { useDispatch } from "react-redux";
import type { AppDispatch } from "./store/store";
import { useEffect } from "react";
import { getCurrentUser } from "./api/authApi";
import { clearUser, setUser } from "./features/auth/authSlice";
import PublicRoute from "./routes/PublicRoute";

function App() {
  const dispatch = useDispatch<AppDispatch>();

  useEffect(() => {
    async function initializeAuth() {
      try {
        const user = await getCurrentUser();

        dispatch(setUser(user));
      } catch {
        dispatch(clearUser());
      }
    }

    initializeAuth();
  }, [dispatch]);

  return (
    <Routes>
      <Route element={<PublicRoute />}>
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/login" element={<LoginPage />} />
      </Route>
      <Route element={<ProtectedRoute />}>
        <Route path="/" element={<Home />} />
      </Route>
    </Routes>
  );
}

export default App;
