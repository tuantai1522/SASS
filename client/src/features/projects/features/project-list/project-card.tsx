import { Link } from '@tanstack/react-router'
import { Card } from '#/shared/ui'
import type { GetProjectsItemResponse } from '../project-list/types.ts'
import { formatUnixSecondsToDate } from '#/shared/lib'

type ProjectCardProps = {
  project: GetProjectsItemResponse
}

export function ProjectCard({ project }: ProjectCardProps) {
  return (
    <Card as="article" className="grid gap-4">
      <div className="flex items-center justify-between gap-4">
        <span className="inline-flex items-center justify-center rounded-full bg-(--color-panel) px-3 py-[0.45rem] text-[0.8rem] font-bold text-(--color-heading)">
          {project.code}
        </span>
        <span className="inline-flex items-center justify-center rounded-full bg-(--color-neutral-soft) px-3 py-[0.45rem] text-[0.8rem] font-bold text-(--color-heading)">
          {project.role}
        </span>
      </div>

      <div>
        <h3>{project.title}</h3>
        <p className="support-copy">{project.description}</p>
      </div>

      <div className="grid gap-2" aria-label={`${project.progress}% complete`}>
        <div className="h-[0.7rem] overflow-hidden rounded-full bg-(--color-panel)">
          <span
            className="block h-full rounded-[inherit] bg-[linear-gradient(90deg,var(--color-accent),var(--color-warning))]"
            style={{ width: `${project.progress}%` }}
          />
        </div>
        <span className="text-[0.88rem] text-(--color-text-muted)">
          {project.progress}% complete
        </span>
      </div>

      <div className="flex items-end justify-between gap-4 text-[0.92rem] text-(--color-text-muted)">
        <span>Created {formatUnixSecondsToDate(project.createdAt)}</span>
        <Link
          to="/projects/$projectId"
          params={{ projectId: project.id }}
          className="ghost-button compact-button"
        >
          Open details
        </Link>
      </div>
    </Card>
  )
}
