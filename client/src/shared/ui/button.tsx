import type { ButtonHTMLAttributes, ReactNode } from 'react'
import { cn } from '#/shared/lib'

type ButtonVariant = 'primary' | 'ghost' | 'chip'
type ButtonSize = 'default' | 'compact'

type ButtonProps = ButtonHTMLAttributes<HTMLButtonElement> & {
  variant?: ButtonVariant
  size?: ButtonSize
  active?: boolean
  children: ReactNode
}

const variantClassMap: Record<ButtonVariant, string> = {
  primary: 'primary-button',
  ghost: 'ghost-button',
  chip: 'filter-chip',
}

const sizeClassMap: Record<ButtonSize, string> = {
  default: '',
  compact: 'compact-button',
}

export function Button({
  variant = 'ghost',
  size = 'default',
  active = false,
  className,
  children,
  ...props
}: ButtonProps) {
  return (
    <button
      {...props}
      className={cn(
        variantClassMap[variant],
        sizeClassMap[size],
        active && variant === 'chip' && 'is-active',
        className,
      )}
    >
      {children}
    </button>
  )
}
