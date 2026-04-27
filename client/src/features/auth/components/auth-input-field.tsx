import { forwardRef } from 'react'
import type { InputHTMLAttributes } from 'react'
import { cn } from '#/shared/lib'

type AuthInputFieldProps = InputHTMLAttributes<HTMLInputElement> & {
  label: string
  error?: string
}

export const AuthInputField = forwardRef<HTMLInputElement, AuthInputFieldProps>(
  ({ className, label, error, ...props }, ref) => {
    return (
      <label className="grid w-full gap-2.5">
        <span className="text-left text-[0.92rem] font-bold text-(--color-heading)">
          {label}
        </span>
        <input
          {...props}
          ref={ref}
          className={cn(
            'min-h-[3.35rem] w-full min-w-0 rounded-[18px] border border-(--color-border) bg-(--color-surface-strong) px-4 py-[0.95rem] text-(--color-text) dark:bg-[rgba(34,38,45,0.92)]',
            className,
          )}
        />
        {error ? (
          <span className="flex text-[0.88rem] text-(--color-danger)">{error}</span>
        ) : null}
      </label>
    )
  },
)

AuthInputField.displayName = 'AuthInputField'
