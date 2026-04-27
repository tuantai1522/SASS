import type { ReactNode } from 'react'
import { cn } from '#/shared/lib'

type PanelHeaderProps = {
  title: ReactNode
  actions?: ReactNode
  className?: string
}

export function PanelHeader({ title, actions, className }: PanelHeaderProps) {
  return (
    <div className={cn('flex items-center justify-between gap-4', className)}>
      <div>{title}</div>
      {actions ? <div>{actions}</div> : null}
    </div>
  )
}
