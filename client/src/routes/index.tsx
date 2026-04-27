import { Navigate, createFileRoute } from '@tanstack/react-router'
import { useAuth } from '#/features/auth'
import { defaultProjectsSearch } from '#/features/projects'

export const Route = createFileRoute('/')({
  component: IndexRedirect,
})

function IndexRedirect() {
  const { isAuthenticated, isBootstrapping } = useAuth()

  if (isBootstrapping) {
    return null
  }

  if (isAuthenticated) {
    return <Navigate to="/projects" search={defaultProjectsSearch} />
  }

  return <Navigate to="/sign-in" />
}
