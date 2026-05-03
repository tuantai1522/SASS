import { createRouter as createTanStackRouter } from "@tanstack/react-router";
import { routeTree } from "@/routeTree.gen.ts";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import type { AuthStatus } from "@/features/auths/manage-token";
import { Suspense } from "react";

export type AuthContext = {
  status: AuthStatus;
};

export type RouterContext = {
  auth: AuthContext;
  queryClient: QueryClient;
};

export const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      staleTime: 1000 * 60,
      gcTime: 1000 * 60 * 5,
      refetchOnWindowFocus: false,
      retry: 1,
    },
    mutations: {
      retry: 0,
    },
  },
});

export function createRouter() {
  const router = createTanStackRouter({
    routeTree,
    context: {
      auth: {
        status: "loading",
      },
      queryClient,
    } satisfies RouterContext,
    Wrap: function WrapComponent({ children }) {
      return (
        // Every component in routes can use React Query
        <QueryClientProvider client={queryClient}>
          <Suspense fallback={<p>Loading....</p>}>{children}</Suspense>
        </QueryClientProvider>
      );
    },
  });

  return router;
}

declare module "@tanstack/react-router" {
  interface Register {
    router: ReturnType<typeof createRouter>;
  }
}
