import type { HTMLAttributes, ReactNode } from 'react'
import { cn } from '#/shared/lib'

type PageSectionProps = HTMLAttributes<HTMLElement> & {
  as?: 'section' | 'div' | 'main'
  children: ReactNode
}

export function PageSection({
  as = 'section',
  className,
  children,
  ...props
}: PageSectionProps) {
  const Component = as

  return (
    <Component {...props} className={cn('grid gap-4', className)}>
      {children}
    </Component>
  )
}
