import { SignOut } from '#/features/auth/features/sign-out'
import { ThemeToggle } from '#/features/theme'
import { UserAccountHeader } from './user-account-header.tsx'

export function Topbar() {
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
        <UserAccountHeader />
        <SignOut />
      </div>
    </header>
  )
}
