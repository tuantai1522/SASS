import { type ReactNode, useEffect } from "react";
import { useAuthStore } from "@/features/auths/manage-token";
import { renewAccessToken } from "@/features/auths/renew-access-token";
import type { RenewAccessTokenResponse } from "@/features/auths/renew-access-token";

let bootstrapAuthPromise: Promise<RenewAccessTokenResponse | null> | null =
  null;

function getBootstrapAuthPromise() {
  if (!bootstrapAuthPromise) {
    bootstrapAuthPromise = renewAccessToken()
      .then((data) => data)
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
      const response = await getBootstrapAuthPromise();

      if (cancelled) return;

      if (response) {
        setAuth({ accessToken: response.token, userId: response.userId });
      } else {
        clearAuth();
      }
    }

    void bootstrapAuth();

    return () => {
      cancelled = true;
    };
  }, [setAuth, clearAuth]);

  return children;
}
