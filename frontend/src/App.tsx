import { useSelector } from "react-redux";
import type { RootState } from "./store/store";

import Home from "./pages/Home";
import { Route, Routes } from "react-router-dom";
import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/RegisterPage";
import ProtectedRoute from "./routes/ProtectedRoute";

function App() {
  // const isAuthenticated = useSelector(
  //     (state: RootState) => state.auth.isAuthenticated
  // );

  // return isAuthenticated ? <Home /> : <Register />;

  return (
    <Routes>
      <Route path="/register" element={<RegisterPage />} />
      <Route path="/login" element={<LoginPage />} />
      <Route element={<ProtectedRoute />}>
        <Route path="/" element={<Home />} />
      </Route>
    </Routes>
  );
}

export default App;
