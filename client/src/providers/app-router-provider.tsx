import { useEffect } from "react";
import { useAuthStore } from "@/stores/auths/auth-store.ts";
import { createRouter } from "../router.tsx";
import { RouterProvider } from "@tanstack/react-router";

const router = createRouter();

export function AppRouterProvider() {
  const status = useAuthStore((state) => state.status);
  const hasBootstrapped = useAuthStore((state) => state.hasBootstrapped);

  useEffect(() => {
    if (hasBootstrapped) {
      router.invalidate();
    }
  }, [status, hasBootstrapped]);

  if (!hasBootstrapped) {
    return null;
  }

  return (
    <RouterProvider
      router={router}
      context={{
        auth: {
          status,
        },
      }}
    />
  );
}
