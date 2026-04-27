import type { ReactNode } from 'react'
import { cn } from '#/shared/lib'
import { Badge } from './badge'

type SectionHeaderProps = {
  eyebrow?: string
  title: string
  code: string
  description: ReactNode
  actions?: ReactNode
  className?: string
}

export function SectionHeader({
  eyebrow,
  title,
  code,
  description,
  actions,
  className,
}: SectionHeaderProps) {
  return (
    <div
      className={cn(
        'grid gap-6 lg:flex lg:items-center lg:justify-between',
        className,
      )}
    >
      <div className="max-w-2xl">
        {eyebrow ? <p className="eyebrow">{eyebrow}</p> : null}

        <div className="flex flex-wrap items-center gap-3">
          <h2 className="page-title">{title}</h2>
          <Badge tone="accent" className="font-mono tracking-wide">
            {code}
          </Badge>
        </div>

        {description ? (
          <p className="support-copy mt-2">{description}</p>
        ) : null}
      </div>

      {actions ? (
        <div className="flex shrink-0 items-start lg:justify-center lg:items-center">
          {actions}
        </div>
      ) : null}
    </div>
  )
}
