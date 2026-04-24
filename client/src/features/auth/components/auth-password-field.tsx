import { forwardRef, useState } from 'react'
import type { InputHTMLAttributes } from 'react'
import { cn } from '#/shared/lib/cn'

type AuthPasswordFieldProps = Omit<
  InputHTMLAttributes<HTMLInputElement>,
  'type'
> & {
  label: string
  error?: string
}

export const AuthPasswordField = forwardRef<
  HTMLInputElement,
  AuthPasswordFieldProps
>(({ className, label, error, ...props }, ref) => {
  const [isVisible, setIsVisible] = useState(false)

  return (
    <label className="grid w-full gap-2.5">
      <span className="text-left text-[0.92rem] font-bold text-(--color-heading)">
        {label}
      </span>
      <span className="relative flex items-center">
        <input
          {...props}
          ref={ref}
          type={isVisible ? 'text' : 'password'}
          className={cn(
            'min-h-[3.35rem] w-full min-w-0 rounded-[18px] border border-(--color-border) bg-(--color-surface-strong) px-4 py-[0.95rem] pr-[4.8rem] text-(--color-text) dark:bg-[rgba(34,38,45,0.92)]',
            className,
          )}
        />
        <button
          type="button"
          className="absolute right-[0.55rem] inline-flex h-10 w-10 items-center justify-center rounded-full bg-(--color-accent-soft) p-0 text-(--color-accent-strong) dark:text-(--color-heading)"
          onClick={() => setIsVisible((current) => !current)}
          aria-label={isVisible ? 'Hide password' : 'Show password'}
        >
          {isVisible ? 'Hide' : 'Show'}
        </button>
      </span>
      {error ? (
        <span className="flex text-[0.88rem] text-(--color-danger)">{error}</span>
      ) : null}
    </label>
  )
})

AuthPasswordField.displayName = 'AuthPasswordField'
