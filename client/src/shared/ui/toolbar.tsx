import type { ReactNode } from 'react'
import { cn } from '#/shared/lib'

type ToolbarProps = {
  primary: ReactNode
  secondary?: ReactNode
  className?: string
}

export function Toolbar({ primary, secondary, className }: ToolbarProps) {
  return (
    <div
      className={cn(
        'toolbar-card grid gap-4 rounded-[24px] p-4 lg:grid-cols-[minmax(0,1fr)_auto] lg:items-center',
        className,
      )}
    >
      <div>{primary}</div>
      {secondary ? <div>{secondary}</div> : null}
    </div>
  )
}
