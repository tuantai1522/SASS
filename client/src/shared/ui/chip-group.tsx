import type { ReactNode } from 'react'
import { cn } from '#/shared/lib'

type ChipGroupProps = {
  children: ReactNode
  className?: string
}

export function ChipGroup({ children, className }: ChipGroupProps) {
  return (
    <div
      className={cn(
        'flex flex-wrap gap-2 max-sm:w-full max-sm:overflow-x-auto',
        className,
      )}
    >
      {children}
    </div>
  )
}
