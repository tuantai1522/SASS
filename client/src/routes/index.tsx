import { Navigate, createFileRoute } from '@tanstack/react-router'
import { useAuth } from '#/features/auth'
import { defaultProjectListSearch } from '#/features/projects'

export const Route = createFileRoute('/')({
  component: IndexRedirect,
})

function IndexRedirect() {
  const { isAuthenticated, isBootstrapping } = useAuth()

  if (isBootstrapping) {
    return null
  }

  if (isAuthenticated) {
    return <Navigate to="/projects" search={defaultProjectListSearch} />
  }

  return <Navigate to="/sign-in" />
}
