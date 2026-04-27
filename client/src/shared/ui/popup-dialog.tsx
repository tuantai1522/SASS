import type { ReactNode } from 'react'
import { cn } from '#/shared/lib'
import { Button } from '#/shared/ui/button'
import { Card } from '#/shared/ui/card'

type PopupDialogProps = {
  open: boolean
  title: string
  description?: string
  onClose: () => void
  children: ReactNode
  footer?: ReactNode
  className?: string
}

function CloseIcon() {
  return (
    <svg
      aria-hidden="true"
      viewBox="0 0 24 24"
      className="h-4 w-4"
      fill="none"
      stroke="currentColor"
      strokeWidth="2"
      strokeLinecap="round"
      strokeLinejoin="round"
    >
      <path d="M18 6 6 18" />
      <path d="m6 6 12 12" />
    </svg>
  )
}

export function PopupDialog({
  open,
  title,
  description,
  onClose,
  children,
  footer,
  className,
}: PopupDialogProps) {
  if (!open) {
    return null
  }

  return (
    <div
      className="fixed inset-0 z-50 grid place-items-center bg-black/50 p-4"
      onMouseDown={onClose}
    >
      <Card
        as="section"
        role="dialog"
        aria-modal="true"
        aria-labelledby="popup-dialog-title"
        onMouseDown={(event) => event.stopPropagation()}
        className={cn(
          'grid w-full max-w-3xl gap-5 rounded-[28px] bg-[var(--color-surface)] p-5 sm:p-6',
          className,
        )}
      >
        <div className="flex items-start justify-between gap-4">
          <div className="grid gap-1">
            <p id="popup-dialog-title" className="eyebrow">
              {title}
            </p>
            {description ? <p className="support-copy">{description}</p> : null}
          </div>
          <Button
            type="button"
            variant="ghost"
            size="compact"
            onClick={onClose}
            aria-label="Close popup"
          >
            <CloseIcon />
          </Button>
        </div>

        <div className="grid gap-4">{children}</div>

        {footer ? (
          <div className="flex flex-wrap justify-end gap-2">{footer}</div>
        ) : null}
      </Card>
    </div>
  )
}
