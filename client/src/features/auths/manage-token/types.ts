export type AuthStatus = "loading" | "authenticated" | "unauthenticated";

export type AuthState = {
  accessToken: string | null;
  status: AuthStatus;
  hasBootstrapped: boolean;

  setAuth: (payload: { accessToken: string }) => void;
  setAccessToken: (accessToken: string) => void;
  clearAuth: () => void;
  setHasBootstrapped: (value: boolean) => void;
};
