import type { PropsWithChildren, ReactNode } from 'react'
import { cn } from '#/shared/lib/cn'

type FormFieldProps = PropsWithChildren<{
  label?: ReactNode
  error?: string
  hint?: ReactNode
  className?: string
}>

export function FormField({
  label,
  error,
  hint,
  className,
  children,
}: FormFieldProps) {
  return (
    <label className={cn('grid gap-2', className)}>
      {label ? <span>{label}</span> : null}
      {children}
      {error ? (
        <span className="support-copy text-[var(--color-danger)]">{error}</span>
      ) : null}
      {!error && hint ? <span className="support-copy">{hint}</span> : null}
    </label>
  )
}
