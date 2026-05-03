import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import {
  AppRouterProvider,
  AuthProvider,
  ThemeProvider,
  Toaster,
} from "@/features/shared";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <ThemeProvider>
      {/*AuthProvider runs first to provide status for AppRouterProvider*/}
      <AuthProvider>
        <AppRouterProvider />
        <Toaster />
      </AuthProvider>
    </ThemeProvider>
  </StrictMode>,
);
