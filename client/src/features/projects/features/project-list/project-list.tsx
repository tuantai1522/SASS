import { ProjectCard } from './project-card'
import type { GetProjectsItemResponse } from '../project-list/types.ts'

type ProjectListProps = {
  projects: GetProjectsItemResponse[]
}

export function ProjectList({ projects }: ProjectListProps) {
  return (
    <div className="grid gap-4 lg:grid-cols-2 2xl:grid-cols-3">
      {projects.map((project) => (
        <ProjectCard key={project.id} project={project} />
      ))}
    </div>
  )
}
