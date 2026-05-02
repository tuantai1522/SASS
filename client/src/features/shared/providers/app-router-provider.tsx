import { useEffect } from "react";
import { createRouter } from "@/router";
import { RouterProvider } from "@tanstack/react-router";
import { useAuthStore } from "@/features/auths/manage-token";

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
