import { create } from "zustand";
import type { AuthState } from "./types";

export const useAuthStore = create<AuthState>((set) => ({
  accessToken: null,
  user: null,
  status: "loading",
  hasBootstrapped: false,

  setAuth: (accessToken: string) =>
    set({
      accessToken,
      status: "authenticated",
      hasBootstrapped: true,
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
