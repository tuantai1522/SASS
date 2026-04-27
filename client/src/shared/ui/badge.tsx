import type { HTMLAttributes, ReactNode } from 'react'
import { cn } from '#/shared/lib'

type BadgeTone = 'neutral' | 'accent' | 'warning' | 'danger' | 'success'

type BadgeProps = HTMLAttributes<HTMLSpanElement> & {
  tone?: BadgeTone
  children: ReactNode
}

export function Badge({
  tone = 'neutral',
  className,
  children,
  ...props
}: BadgeProps) {
  return (
    <span {...props} className={cn('badge', `tone-${tone}`, className)}>
      {children}
    </span>
  )
}
