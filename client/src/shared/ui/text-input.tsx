import type { InputHTMLAttributes } from 'react'
import { cn } from '#/shared/lib'

type TextInputProps = InputHTMLAttributes<HTMLInputElement>

export function TextInput({ className, ...props }: TextInputProps) {
  return <input {...props} className={cn('search-input', className)} />
}
