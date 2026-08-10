import { useSelector } from "react-redux";
import type { RootState } from "./store/store";

import Login from "./pages/Login";
import Home from "./pages/Home";

function App() {
    const isAuthenticated = useSelector(
        (state: RootState) => state.auth.isAuthenticated
    );

    return isAuthenticated ? <Home /> : <Login />;
}

export default App;