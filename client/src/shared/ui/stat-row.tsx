import type { ReactNode } from 'react'
import { Card } from '#/shared/ui/card'
import { cn } from '#/shared/lib/cn'

type StatItem = {
  label: string
  value: ReactNode
}

type StatRowProps = {
  items: StatItem[]
  className?: string
}

export function StatRow({ items, className }: StatRowProps) {
  return (
    <div className={cn('grid gap-4 md:grid-cols-2', className)}>
      {items.map((item) => (
        <Card key={item.label} className="bg-[var(--color-panel)]">
          <span className="meta-label">{item.label}</span>
          <strong>{item.value}</strong>
        </Card>
      ))}
    </div>
  )
}
