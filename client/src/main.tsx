import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import {
  AppRouterProvider,
  AuthProvider,
  NuqsProvider,
  RealtimeProvider,
  ThemeProvider,
  Toaster,
} from "@/features/shared";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <ThemeProvider>
      {/*AuthProvider runs first to provide status for AppRouterProvider*/}
      <AuthProvider>
        <RealtimeProvider>
          <NuqsProvider>
            <AppRouterProvider />
          </NuqsProvider>
          <Toaster />
        </RealtimeProvider>
      </AuthProvider>
    </ThemeProvider>
  </StrictMode>,
);
