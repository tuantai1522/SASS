import type { ReactNode } from 'react'
import { cn } from '#/shared/lib/cn'

type SectionHeaderProps = {
  eyebrow?: string
  title: string
  description: ReactNode
  actions?: ReactNode
  className?: string
}

export function SectionHeader({
  eyebrow,
  title,
  description,
  actions,
  className,
}: SectionHeaderProps) {
  return (
    <div
      className={cn(
        'grid gap-4 lg:flex lg:items-start lg:justify-between',
        className,
      )}
    >
      <div>
        {eyebrow ? <p className="eyebrow">{eyebrow}</p> : null}
        <h2 className="page-title">{title}</h2>
        <p className="support-copy">{description}</p>
      </div>
      {actions ? <div>{actions}</div> : null}
    </div>
  )
}
