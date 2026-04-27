import { createFileRoute } from '@tanstack/react-router'

import type { GetProjectsRequest } from '#/features/projects'
import { GetProjectsRequestSchema } from '#/features/projects'
import { ProjectWorkspace } from '#/features/projects/features/project-list/project-workspace.tsx'

export const Route = createFileRoute('/_app/projects/')({
  validateSearch: (search): GetProjectsRequest => {
    return GetProjectsRequestSchema.parse(search)
  },
  component: ProjectsPage,
})

function ProjectsPage() {
  const search = Route.useSearch()
  const navigate = Route.useNavigate()

  const updateSearch = (nextSearch: Partial<GetProjectsRequest>) => {
    navigate({
      search: (currentSearch) => ({
        ...currentSearch,
        ...nextSearch,
      }),
    })
  }

  return (
    <>
      <ProjectWorkspace search={search} updateSearch={updateSearch} />
    </>
  )
}
