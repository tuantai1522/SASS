import { SignOut } from '#/features/auth/features/sign-out'
import { ThemeToggle, useTheme } from '#/features/theme'

export function Topbar() {
  const { resolvedTheme } = useTheme()

  return (
    <header className="flex justify-between rounded-[26px] border border-(--color-border) bg-(--color-surface) px-5 py-5 shadow-(--shadow-md) backdrop-blur-[18px]">
      <div>
        <p className="eyebrow">Product overview</p>
        <p className="m-0 font-bold text-(--color-heading)">
          Projects, tasks, and team conversations in one place
        </p>
      </div>

      <div className="grid gap-4 md:flex md:items-center md:justify-end">
        <ThemeToggle />
        <div className="flex items-center gap-4">
          <div
            className="grid h-10 w-10 place-items-center rounded-full bg-(--color-accent-soft) font-bold text-(--color-accent-strong)"
            aria-hidden="true"
          >
            TN
          </div>
          <div>
            <p className="m-0 font-bold text-(--color-heading)">Team Nguyen</p>
            <p className="m-0 text-[0.85rem] text-(--color-text-muted)">
              Theme {resolvedTheme}
            </p>
          </div>
        </div>
        <SignOut />
      </div>
    </header>
  )
}
