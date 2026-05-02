import { type ReactNode, useEffect } from "react";
import { useAuthStore } from "@/features/auths/manage-token";
import { renewAccessToken } from "@/features/auths/renew-access-token";

export function AuthProvider({ children }: { children: ReactNode }) {
  const setAuth = useAuthStore((state) => state.setAuth);
  const clearAuth = useAuthStore((state) => state.clearAuth);

  useEffect(() => {
    let cancelled = false;

    async function bootstrapAuth() {
      try {
        const data = await renewAccessToken();

        if (cancelled) return;

        setAuth({
          accessToken: data.token,
        });
      } catch {
        if (cancelled) return;

        clearAuth();
      }
    }

    bootstrapAuth();

    return () => {
      cancelled = true;
    };
  }, [setAuth, clearAuth]);

  return children;
}
