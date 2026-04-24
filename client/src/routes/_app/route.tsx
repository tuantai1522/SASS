import { Navigate, createFileRoute } from '@tanstack/react-router'
import { useAuth } from '#/features/auth'
import { AppShell } from '#/features/shell'

export const Route = createFileRoute('/_app')({
  component: AppRoute,
})

function AppRoute() {
  const { isAuthenticated, isBootstrapping } = useAuth()

  if (isBootstrapping) {
    return null
  }

  if (!isAuthenticated) {
    return <Navigate to="/sign-in" />
  }

  return <AppShell />
}
