export type AuthStatus = "loading" | "authenticated" | "unauthenticated";

export type AuthState = {
  accessToken: string | null;
  userId: string | null;
  status: AuthStatus;
  hasBootstrapped: boolean;

  setAuth: (payload: { accessToken: string; userId: string }) => void;
  clearAuth: () => void;
  setHasBootstrapped: (value: boolean) => void;
};
