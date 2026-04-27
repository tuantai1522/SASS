import { createFileRoute } from '@tanstack/react-router'
import { ProjectTasksScreen } from '#/features/projects'

export const Route = createFileRoute('/_app/projects/$projectId/tasks')({
  component: ProjectTasksScreen,
})
