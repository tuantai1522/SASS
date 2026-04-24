import { useAuth } from '#/features/auth'
import { defaultProjectListSearch } from '#/features/projects'
import { Navigate, Outlet, createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/_public')({
  component: PublicRoute,
})

function PublicRoute() {
  const { isAuthenticated, isBootstrapping } = useAuth()

  if (isBootstrapping) {
    return null
  }

  if (isAuthenticated) {
    return <Navigate to="/projects" search={defaultProjectListSearch} />
  }

  return <Outlet />
}
