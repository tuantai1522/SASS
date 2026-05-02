import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { ThemeProvider } from "@/providers/theme-provider.tsx";
import { AppRouterProvider } from "@/providers/app-router-provider.tsx";
import { AuthProvider } from "@/providers/auth-provider.ts";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <ThemeProvider>
      {/*AuthProvider runs first to provide status for AppRouterProvider*/}
      <AuthProvider>
        <AppRouterProvider />
      </AuthProvider>
    </ThemeProvider>
  </StrictMode>,
);
