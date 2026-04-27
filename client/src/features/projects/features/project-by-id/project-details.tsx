import { useSuspenseQuery } from '@tanstack/react-query'

import {
  Button,
  PageSection,
  SectionHeader, StatRow,
} from '#/shared/ui'
import { projectByIdOptions } from '#/features/projects'

type ProjectDetailsProps = {
  projectId: string
}

export function ProjectDetails({ projectId }: ProjectDetailsProps) {
  const { data: project } = useSuspenseQuery(projectByIdOptions(projectId))

  return (
    <PageSection>
      <SectionHeader
        eyebrow="Project workspace"
        title={project.title}
        code={project.code}
        description={project.description}
        actions={
          <div className="flex flex-col items-start gap-3 lg:items-end">
            <Button type="button" variant="primary">
              New task
            </Button>
          </div>
        }
      />
      <StatRow
        items={[
          { label: 'Role', value: project.role },
          { label: 'Progress', value: `${project.progress}%` },
          { label: 'Tasks', value: project.totalTasks },
          { label: 'Completed', value: project.totalCompletedTasks },
        ]}
        className="xl:grid-cols-4"
      />
    </PageSection>
  )
}
