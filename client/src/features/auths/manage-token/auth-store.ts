import { create } from "zustand";
import type { AuthState } from "./types";

export const useAuthStore = create<AuthState>((set) => ({
  accessToken: null,
  userId: null,
  user: null,
  status: "loading",
  hasBootstrapped: false,

  setAuth: (payload: { accessToken: string; userId: string }) =>
    set({
      accessToken: payload.accessToken,
      userId: payload.userId,
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
