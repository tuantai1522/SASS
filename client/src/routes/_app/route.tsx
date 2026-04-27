import { createFileRoute, redirect } from '@tanstack/react-router'
import { ensureAuthReady } from '#/features/auth/features/session'
import { AppShell, getMe } from '#/features/shell'
import { queryKeys } from '#/shared/lib'

export const Route = createFileRoute('/_app')({
  // To validate authentication before accessing resources
  beforeLoad: async () => {
    const authState = await ensureAuthReady()

    if (!authState.isAuthenticated) {
      throw redirect({ to: '/sign-in' })
    }
  },
  component: AppRoute,

  loader: async ({ context: { queryClient }}) => {
    await queryClient.prefetchQuery({
      queryKey: queryKeys.auth.me(),
      queryFn: () => getMe(),
    })
  },
})

function AppRoute() {
  return <AppShell />
}
