import { type ReactNode, useEffect } from "react";
import { useAuthStore } from "@/features/auths/manage-token";
import { renewAccessToken } from "@/features/auths/renew-access-token";

let bootstrapAuthPromise: Promise<string | null> | null = null;

function getBootstrapAuthPromise() {
  if (!bootstrapAuthPromise) {
    bootstrapAuthPromise = renewAccessToken()
      .then((data) => data.token)
      .catch(() => null);
  }

  return bootstrapAuthPromise;
}

export function AuthProvider({ children }: { children: ReactNode }) {
  const setAuth = useAuthStore((state) => state.setAuth);
  const clearAuth = useAuthStore((state) => state.clearAuth);

  useEffect(() => {
    let cancelled = false;

    async function bootstrapAuth() {
      const token = await getBootstrapAuthPromise();

      if (cancelled) return;

      if (token) {
        setAuth(token);
      } else {
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
