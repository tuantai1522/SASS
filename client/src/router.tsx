import { createRouter as createTanStackRouter } from "@tanstack/react-router";
import { routeTree } from "@/routeTree.gen.ts";
import type { AuthStatus } from "@/stores/auths/auth-store.ts";

export type AuthContext = {
  status: AuthStatus;
};

export type RouterContext = {
  auth: AuthContext;

  // Todo: To add later
  // queryClient: QueryClient
};

export function createRouter() {
  const router = createTanStackRouter({
    routeTree,
    context: {
      auth: {
        status: "loading",
      },
    } satisfies RouterContext,
    // Wrap: function WrapComponent({ children }) {
    //   return (
    //     // Every component in routes can use React Query
    //     <QueryClientProvider client={queryClient}>
    //       {/* Todo: To add Spinner Suspense */}
    //       <Suspense fallback={<p>Loading....</p>}>{children}</Suspense>
    //   </QueryClientProvider>
    // )
    // },
  });

  return router;
}

declare module "@tanstack/react-router" {
  interface Register {
    router: ReturnType<typeof createRouter>;
  }
}
