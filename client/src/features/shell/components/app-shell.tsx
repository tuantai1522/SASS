import { Outlet } from '@tanstack/react-router'
import { SidebarNav } from './sidebar-nav'
import { Topbar } from './topbar'

export function AppShell() {
  return (
    <div className="grid gap-5 p-4 xl:grid-cols-[360px_minmax(0,1fr)] xl:p-5">
      <aside className="flex flex-col gap-5 rounded-[28px] border border-(--color-border) bg-(--color-surface) p-5 shadow-(--shadow-md) backdrop-blur-[18px]">
        <div className="flex items-start gap-4">
          <div className="grid h-12 w-12 place-items-center rounded-[18px] bg-[linear-gradient(135deg,var(--color-accent),var(--color-warning))] font-extrabold text-[#fffdf9]">
            S
          </div>
          <div>
            <p className="eyebrow">SASS workspace</p>
            <h1 className="font-(family-name:--font-display) text-[1.75rem] leading-[1.05] tracking-[-0.03em]">
              Studio for delivery and chat
            </h1>
          </div>
        </div>

        <SidebarNav />

        <div className="future-panel rounded-[22px] border border-(--color-border) bg-(--color-panel) p-[1.15rem]">
          <p className="eyebrow">Workspace note</p>
          <h2 className="m-0 text-[1.15rem] text-(--color-heading)">
            Communication panel
          </h2>
          <p className="mt-3 text-(--color-text-muted) leading-[1.65]">
            Keep project updates and team context in one place without leaving
            the delivery flow.
          </p>
        </div>
      </aside>

      <div className="flex flex-col gap-8">
        <Topbar />

        <main className="min-h-0">
          <Outlet />
        </main>
      </div>
    </div>
  )
}
