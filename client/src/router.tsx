import { createRouter as createTanStackRouter } from '@tanstack/react-router'

import { routeTree } from '#/routeTree.gen'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
import { Suspense } from 'react'

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
})

export function createRouter() {
  const router = createTanStackRouter({
    routeTree,
    context: {
      queryClient,
    },
    Wrap: function WrapComponent({ children }) {
      return (
        // Every component in routes can use React Query
        <QueryClientProvider client={queryClient}>
          {/* Todo: To add Spinner Suspense */}
          <Suspense fallback={<p>Loading....</p>}>{children}</Suspense>
        </QueryClientProvider>
      )
    },
  })

  return router
}

declare module '@tanstack/react-router' {
  interface Register {
    router: ReturnType<typeof createRouter>
  }
}
