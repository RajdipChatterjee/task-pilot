import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { FluentProvider, webLightTheme } from "@fluentui/react-components";
import { Provider } from "react-redux";

import App from "./App";
import { store } from "./store/store";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <Provider store={store}>
      <FluentProvider theme={webLightTheme}>
        <App />
      </FluentProvider>
    </Provider>
  </StrictMode>,
);
