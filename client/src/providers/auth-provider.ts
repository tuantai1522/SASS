import { useEffect } from "react";
import { useAuthStore } from "@/stores/auths/auth-store.ts";
import { renewAccessToken } from "@/api/auths/auth.ts";

export function AuthProvider({ children }: { children: React.ReactNode }) {
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
