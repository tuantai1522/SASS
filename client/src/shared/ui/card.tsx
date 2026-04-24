import type { HTMLAttributes, ReactNode } from 'react'
import { cn } from '#/shared/lib/cn'

type CardProps = HTMLAttributes<HTMLElement> & {
  as?: 'article' | 'section' | 'aside' | 'div'
  children: ReactNode
}

export function Card({ as = 'div', className, children, ...props }: CardProps) {
  const Component = as

  return (
    <Component
      {...props}
      className={cn('content-card rounded-[24px] p-4', className)}
    >
      {children}
    </Component>
  )
}
