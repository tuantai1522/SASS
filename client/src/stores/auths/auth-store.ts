import { create } from "zustand";

export type AuthStatus = "loading" | "authenticated" | "unauthenticated";

type AuthState = {
  accessToken: string | null;
  status: AuthStatus;
  hasBootstrapped: boolean;

  setAuth: (payload: { accessToken: string }) => void;
  setAccessToken: (accessToken: string) => void;
  clearAuth: () => void;
  setHasBootstrapped: (value: boolean) => void;
};

export const useAuthStore = create<AuthState>((set) => ({
  accessToken: null,
  user: null,
  status: "loading",
  hasBootstrapped: false,

  setAuth: ({ accessToken }) =>
    set({
      accessToken,
      status: "authenticated",
      hasBootstrapped: true,
    }),

  setAccessToken: (accessToken) =>
    set({
      accessToken,
      status: "authenticated",
    }),

  clearAuth: () =>
    set({
      accessToken: null,
      status: "unauthenticated",
      hasBootstrapped: true,
    }),

  setHasBootstrapped: (value) =>
    set({
      hasBootstrapped: value,
    }),
}));
