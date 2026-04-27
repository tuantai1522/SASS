import { createFileRoute } from '@tanstack/react-router'
import { z } from 'zod'
import { ProjectDetails, projectByIdOptions } from '#/features/projects'

export const Route = createFileRoute('/_app/projects/$projectId/')({
  component: ProjectOverviewPage,

  params: {
    parse: ({ projectId }) => ({
      projectId: z.coerce.string().parse(projectId),
    }),
  },

  // Prefetch project data by project ID
  loader: async ({ context: { queryClient }, params: { projectId } }) => {
    await queryClient.prefetchQuery(projectByIdOptions(projectId))
  },
})

function ProjectOverviewPage() {
  const { projectId } = Route.useParams()

  return (
    <>
      <ProjectDetails projectId={projectId} />
    </>
  )
}
